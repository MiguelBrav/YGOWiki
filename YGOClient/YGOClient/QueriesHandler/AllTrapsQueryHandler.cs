using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllTrapsQueryHandler : IRequestHandler<AllTrapsQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public AllTrapsQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> Handle(AllTrapsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                AllTrapTypeReply response = await _client.GetAllTrapCardsAsync(new ByLanguageId { LanguageId = request.LanguageId });

                if(response.TrapTypes.Count == 0)
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
