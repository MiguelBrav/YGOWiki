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
     public class AllSpecialMonsterCardsPageQueryHandler : IRequestHandler<AllSpecialMonsterCardsPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<SpecialMonsterTypeDetail> _paginationService;
        public AllSpecialMonsterCardsPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<SpecialMonsterTypeDetail> paginationService)
        {
            _client = client;
            _paginationService = paginationService;
        }

        public async Task<ApiResponse> Handle(AllSpecialMonsterCardsPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllSpecialMonsterTypeReply response = await _client.GetAllSpecialMonstersAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<SpecialMonsterTypeDetail> specialMonsters =  _paginationService.GetPagedData(response.SpecialMonsterTypes, request.PageId, request.PageSize);
                PagedResult<SpecialMonsterTypeDetail> specialMonsters = _paginationService.GetPagedResult(response.SpecialMonsterTypes, request.PageId, request.PageSize);


                if (response.SpecialMonsterTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.SpecialMonsterTypes.Clear();

                //response.SpecialMonsterTypes.AddRange(specialMonsters);

                var jsonResponse = JsonSerializer.Serialize(specialMonsters, new JsonSerializerOptions
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
