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
     public class AllMonsterTypesPageQueryHandler : IRequestHandler<AllMonsterTypesPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<MonsterTypeDetail> _paginationService;
        public AllMonsterTypesPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<MonsterTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllMonsterTypesPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllMonsterTypeReply response = await _client.GetAllMonsterTypesAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<MonsterTypeDetail> banlistPage = _paginationService.GetPagedData(response.MonsterTypes, request.PageId, request.PageSize);
                PagedResult<MonsterTypeDetail> monsterTypePage = _paginationService.GetPagedResult(response.MonsterTypes, request.PageId, request.PageSize);


                if (response.MonsterTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.MonsterTypes.Clear();

                //response.MonsterTypes.AddRange(banlistPage);

                var jsonResponse = JsonSerializer.Serialize(monsterTypePage, new JsonSerializerOptions
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
