using Grpc.Core;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
    public class MonsterCardByIdQueryHandler : IRequestHandler<MonsterCardByIdQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public MonsterCardByIdQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public async Task<ApiResponse> Handle(MonsterCardByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                MonsterCardDetail response = await _client.GetMonsterCardTypeAsync(new ByLanguageIdAndId { LanguageId = request.LanguageId, Id = request.Id });

                var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;
            }
            catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled && !string.IsNullOrEmpty(ex.Status.Detail))
            {
                grpcResponse.StatusCode = 404;
                grpcResponse.ResponseMessage = ex.Status.Detail;
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
