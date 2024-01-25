using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllSpellsQueryHandler : IRequestHandler<AllSpellsQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public AllSpellsQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> Handle(AllSpellsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllSpellTypeReply response = await _client.GetAllSpellCardsAsync(new ByLanguageId { LanguageId = request.LanguageId });

                if(response.SpellTypes.Count == 0)
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
