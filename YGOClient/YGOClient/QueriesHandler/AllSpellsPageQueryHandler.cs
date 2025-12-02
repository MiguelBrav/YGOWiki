using Google.Protobuf.Collections;
using Grpc.Net.Client;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Interfaces;
using YGOClient.Models;
using YGOClient.Queries;

namespace YGOClient.QueriesHandler
{
     public class AllSpellsPageQueryHandler : IRequestHandler<AllSpellsPageQuery, ApiResponse>
    {
        private readonly YGOWiki.YGOWikiClient _client;
        private readonly IPaginationService<SpellTypeDetail> _paginationService;
        private readonly ICacheService _cacheService;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly int _cacheMinutes;
        public AllSpellsPageQueryHandler(YGOWiki.YGOWikiClient client, IPaginationService<SpellTypeDetail> paginationService, ICacheService cacheService, IConfiguration config, IMemoryCache cache)
        {
            _client = client;
            _paginationService = paginationService;
            _cacheService = cacheService;
            _config = config;
            _cacheMinutes = _config.GetValue<int>("CacheSettings:Minutes");
            _cache = cache;
        }

        public async Task<ApiResponse> Handle(AllSpellsPageQuery request, CancellationToken cancellationToken)
        {
            ApiResponse grpcResponse = new ApiResponse();

            try
            {
                string cacheKey = await _cacheService.Generate(request);

                if (_cache.TryGetValue(cacheKey, out ApiResponse cachedResponse))
                {
                    return cachedResponse;
                }

                AllSpellTypeReply response = await _client.GetAllSpellCardsAsync(new ByLanguageId { LanguageId = request.LanguageId });

                //RepeatedField<SpellTypeDetail> spellsPage =  _paginationService.GetPagedData(response.SpellTypes, request.PageId, request.PageSize);
                PagedResult<SpellTypeDetail> spellsPage = _paginationService.GetPagedResult(response.SpellTypes, request.PageId, request.PageSize);


                if (response.SpellTypes.Count == 0)
                {
                    grpcResponse.StatusCode = 204;
                    grpcResponse.ResponseMessage = "No content";
                    grpcResponse.Response = true;

                    return grpcResponse;

                }

                // Pagination
                //response.SpellTypes.Clear();

                //response.SpellTypes.AddRange(spellsPage);

                var jsonResponse = JsonSerializer.Serialize(spellsPage, new JsonSerializerOptions
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
