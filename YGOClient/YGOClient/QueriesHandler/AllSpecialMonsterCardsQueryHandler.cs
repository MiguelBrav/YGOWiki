using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllSpecialMonsterCardsQueryHandler : IRequestHandler<AllSpecialMonsterCardsQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public AllSpecialMonsterCardsQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> Handle(AllSpecialMonsterCardsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllSpecialMonsterTypeReply response = await _client.GetAllSpecialMonstersAsync(new ByLanguageId { LanguageId = request.LanguageId });

                if(response.SpecialMonsterTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;
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
