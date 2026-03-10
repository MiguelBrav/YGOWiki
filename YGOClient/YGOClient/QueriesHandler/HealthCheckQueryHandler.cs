using System;
using System.Text.Json;
using ServerYGO;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
    public class HealthCheckQueryHandler : UseCaseBase<HealthCheckQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;

        public HealthCheckQueryHandler(YGOWiki.YGOWikiClient client)
        {
            _client = client;
        }

        public override async Task<ApiResponse> Execute(HealthCheckQuery request)
        {
            var apiResponse = new ApiResponse();

            try
            {
                // Check gRPC availability 
                var grpcHealthy = false;
                string grpcException = null;

                try
                {
                    var resp = await _client.GetAllTrapCardsAsync(new ByLanguageId { LanguageId = "en-Us" });
                    grpcHealthy = true;
                }
                catch (Exception ex)
                {
                    grpcHealthy = false;
                    var msg = ex.Message ?? string.Empty;
                    msg = msg.Replace("\\u0022", "\"").Replace("\\\"", "\"");
                    grpcException = msg;
                }

                var overallStatus = grpcHealthy ? "Healthy" : "Unhealthy";

                var checks = new[]
                {
                    new {
                        name = "Application",
                        status = "Healthy",
                        description = "API is running.",
                        exception = (string?)null
                    },
                    new {
                        name = "YGOWiki gRPC",
                        status = grpcHealthy ? "Healthy" : "Unhealthy",
                        description = grpcHealthy ? "Successfully connected to the gRPC server." : "Could not connect to the gRPC server.",
                        exception = grpcException
                    }
                };

                var payload = new {
                    status = overallStatus,
                    checks = checks
                };

                apiResponse.StatusCode = 200;
                apiResponse.Response = true;
                apiResponse.ResponseMessage = JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            }
            catch (Exception ex)
            {
                apiResponse.StatusCode = 500;
                apiResponse.Response = false;
                apiResponse.ResponseMessage = JsonSerializer.Serialize(new {
                    status = "Unhealthy",
                    checks = new[] { new { name = "HealthCheck", status = "Unhealthy", description = "Unhandled exception during health check.", exception = ex.Message } }
                }, new JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            }

            return apiResponse;
        }
    }
}
