using Google.Protobuf.Collections;
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
     public class AllTypeCardsPageQueryHandler : IRequestHandler<AllTypeCardsPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<CardTypeDetail> _paginationService;
        public AllTypeCardsPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<CardTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllTypeCardsPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllTypeCardsReply response = await _client.GetAllTypeCardsAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<CardTypeDetail> trapsPage =  _paginationService.GetPagedData(response.CardTypes, request.PageId, request.PageSize);

                PagedResult<CardTypeDetail> typeCardsPage = _paginationService.GetPagedResult(response.CardTypes, request.PageId, request.PageSize);

                if (response.CardTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.CardTypes.Clear();

                //response.CardTypes.AddRange(typeCardsPage);

                var jsonResponse = JsonSerializer.Serialize(typeCardsPage, new JsonSerializerOptions
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
