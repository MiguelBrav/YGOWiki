using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllTrapsPageQueryHandler : IRequestHandler<AllTrapsPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<TrapTypeDetail> _paginationService;
        public AllTrapsPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<TrapTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllTrapsPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllTrapTypeReply response = await _client.GetAllTrapCardsAsync(new ByLanguageId { LanguageId = request.LanguageId });

                RepeatedField<TrapTypeDetail> trapsPage =  _paginationService.GetPagedData(response.TrapTypes, request.PageId, request.PageSize);
                           
                if (response.TrapTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                response.TrapTypes.Clear();

                response.TrapTypes.AddRange(trapsPage);

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
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
