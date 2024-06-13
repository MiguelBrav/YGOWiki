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
     public class AllMonsterCardsPageQueryHandler : IRequestHandler<AllMonsterCardsPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<MonsterCardDetail> _paginationService;
        public AllMonsterCardsPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<MonsterCardDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllMonsterCardsPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllMonsterCardTypeReply response = await _client.GetAllMonsterCardTypesAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<MonsterCardDetail> monsterCards = _paginationService.GetPagedData(response.MonsterCardTypes, request.PageId, request.PageSize);
                PagedResult<MonsterCardDetail> monsterCards = _paginationService.GetPagedResult(response.MonsterCardTypes, request.PageId, request.PageSize);


                if (response.MonsterCardTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.MonsterCardTypes.Clear();

                //response.MonsterCardTypes.AddRange(monsterCards);

                var jsonResponse = JsonSerializer.Serialize(monsterCards, new JsonSerializerOptions
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
