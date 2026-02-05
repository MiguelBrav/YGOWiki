using Microsoft.Extensions.Caching.Memory;
using ServerYGO;
using System.Text.Json;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Models;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllAttributesPageQueryHandler : UseCaseBase<AllAttributesPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<AttributeDetail> _paginationService;
        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly int _cacheMinutes;


        public AllAttributesPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<AttributeDetail> paginationService, ICacheService cacheService, IConfiguration config, IMemoryCache cache)
        {
            _client = client;
            _paginationService = paginationService;
            _cacheService = cacheService;
            _config = config;
            _cacheMinutes = _config.GetValue<int>("CacheSettings:Minutes");
            _cache = cache;
        }

        public override async Task<ApiResponse> Execute(AllAttributesPageQuery request)
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

                //RepeatedField<AttributeDetail> attributesPage =  _paginationService.GetPagedData(response.Attributes, request.PageId, request.PageSize);
                PagedResult<AttributeDetail> attributesPage = _paginationService.GetPagedResult(response.Attributes, request.PageId, request.PageSize);


                if (response.Attributes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.Attributes.Clear();

                //response.Attributes.AddRange(attributesPage);

                var jsonResponse = JsonSerializer.Serialize(attributesPage, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                });

                grpcResponse.StatusCode = 200;
                grpcResponse.ResponseMessage = jsonResponse;
                grpcResponse.Response = true;

                _cache.Set(cacheKey, grpcResponse, TimeSpan.FromMinutes(_cacheMinutes));

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
