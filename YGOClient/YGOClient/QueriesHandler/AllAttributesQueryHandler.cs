using Microsoft.Extensions.Caching.Memory;
using ServerYGO;
using System.Text.Json;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllAttributesQueryHandler : UseCaseBase<AllAttributesQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly int _cacheMinutes;

        public AllAttributesQueryHandler(YGOWiki.YGOWikiClient client, ICacheService cacheService, IConfiguration config, IMemoryCache cache)
        {
            _client = client;
            _cacheService = cacheService;
            _config = config;
            _cacheMinutes = _config.GetValue<int>("CacheSettings:Minutes");
            _cache = cache;
        }

        public override async Task<ApiResponse> Execute(AllAttributesQuery request)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                string cacheKey = await _cacheService.Generate(request);

                if (_cache.TryGetValue(cacheKey, out ApiResponse cachedResponse))
                {
                    return cachedResponse;
                }

                AllAttributeReply response = await _client.GettAllAttributesAsync(new ByLanguageId { LanguageId = request.LanguageId });

                if(response.Attributes.Count == 0)
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

                _cache.Set(cacheKey, grpcResponse, TimeSpan.FromMinutes(_cacheMinutes));

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
