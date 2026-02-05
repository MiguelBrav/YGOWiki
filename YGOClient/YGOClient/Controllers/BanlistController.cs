using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.Aggregator;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BanlistController : ControllerBase
    {

        private readonly IBanlistAggregator _aggregator;


        public BanlistController(IBanlistAggregator aggregator)
        {
            _aggregator = aggregator;
        }
        /// <summary>
        /// Get all banlist translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllBanlist(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllBanlistQuery query = new AllBanlistQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _aggregator.AllBanlistQuery(query);

            if (response.StatusCode == 204)
            {
                return StatusCode(response.StatusCode);
            }
            if (response.Response == null | response.Response is false)
            {
                return StatusCode(response.StatusCode, response.ResponseMessage);
            }

            return StatusCode(response.StatusCode, response.ResponseMessage);

        }

        /// <summary>
        /// Get paginated banlist translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetBanlistsPageById(string languageId, int pageId, int pageSize)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            if (pageSize <= 0)
            {
                return BadRequest("Page size must be greater than 0");
            }

            if (pageId <= 0)
            {
                return BadRequest("PageId must be greater than 0");
            }

            AllBanlistPageQuery query = new AllBanlistPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _aggregator.AllBanlistPageQuery(query);

            if (response.StatusCode == 204)
            {
                return StatusCode(response.StatusCode);
            }
            if (response.Response == null | response.Response is false)
            {
                return StatusCode(response.StatusCode, response.ResponseMessage);
            }

            return StatusCode(response.StatusCode, response.ResponseMessage);

        }

        /// <summary>
        /// Get banlist translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{banlistId}/{languageId}")]
        public async Task<IActionResult> GetBanlistById(string languageId, int banlistId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            BanlistByIdQuery query = new BanlistByIdQuery()
            {
                LanguageId = languageId,
                Id = banlistId
            };

            ApiResponse response = await _aggregator.BanlistByIdQuery(query);

            if (response.StatusCode == 204)
            {
                return StatusCode(response.StatusCode);
            }
            if (response.Response == null | response.Response is false)
            {
                return StatusCode(response.StatusCode, response.ResponseMessage);
            }

            return StatusCode(response.StatusCode, response.ResponseMessage);

        }

    }
}