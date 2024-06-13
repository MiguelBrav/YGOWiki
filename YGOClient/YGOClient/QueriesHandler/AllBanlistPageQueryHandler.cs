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
     public class AllBanlistPageQueryHandler : IRequestHandler<AllBanlistPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<BanlistTypeDetail> _paginationService;
        public AllBanlistPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<BanlistTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllBanlistPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllBanlistReply response = await _client.GetAllBanlistAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<BanlistTypeDetail> banlistPage = _paginationService.GetPagedData(response.BanlistTypes, request.PageId, request.PageSize);
                PagedResult<BanlistTypeDetail> banlistPage = _paginationService.GetPagedResult(response.BanlistTypes, request.PageId, request.PageSize);


                if (response.BanlistTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.BanlistTypes.Clear();

                //response.BanlistTypes.AddRange(banlistPage);

                var jsonResponse = JsonSerializer.Serialize(banlistPage, new JsonSerializerOptions
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
