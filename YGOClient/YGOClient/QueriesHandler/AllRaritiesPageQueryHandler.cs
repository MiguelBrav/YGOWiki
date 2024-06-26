﻿using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Models;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllRaritiesPageQueryHandler : IRequestHandler<AllRaritiesPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<RarityTypeDetail> _paginationService;
        public AllRaritiesPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<RarityTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllRaritiesPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllRarityReply response = await _client.GetAllRaritiesAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<RarityTypeDetail> raritiesPage =  _paginationService.GetPagedData(response.Rarities, request.PageId, request.PageSize);
                PagedResult<RarityTypeDetail> raritiesPage = _paginationService.GetPagedResult(response.Rarities, request.PageId, request.PageSize);


                if (response.Rarities.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.Rarities.Clear();

                //response.Rarities.AddRange(raritiesPage);

                var jsonResponse = JsonSerializer.Serialize(raritiesPage, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                grpcResponse.StatusCode = 405;
                grpcResponse.ResponseMessage = ex.Message;
                grpcResponse.Response = false;
            }
            catch (Exception)
            {
                grpcResponse.StatusCode = 500; 
                grpcResponse.ResponseMessage = "Error";
                grpcResponse.Response = false;
            }

            return grpcResponse;

        }
    }
}
