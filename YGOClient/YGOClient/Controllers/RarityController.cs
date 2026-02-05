using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RarityController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllRaritiesQueryHandler _allRaritiesQueryHandler;
        private readonly AllRaritiesPageQueryHandler _allRaritiesPageQueryHandler;
        private readonly RarityByIdQueryHandler _rarityByIdQueryHandler;

        public RarityController(UseCaseDispatcher dispatcher, AllRaritiesQueryHandler allRaritiesQueryHandler, AllRaritiesPageQueryHandler allRaritiesPageQueryHandler, RarityByIdQueryHandler rarityByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allRaritiesQueryHandler = allRaritiesQueryHandler;
            _allRaritiesPageQueryHandler = allRaritiesPageQueryHandler;
            _rarityByIdQueryHandler = rarityByIdQueryHandler;
        }
        /// <summary>
        /// Get all rarities translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllRarities(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllRaritiesQuery query = new AllRaritiesQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allRaritiesQueryHandler, query);

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
        /// Get paginated rarities translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetRaritiesPageById(string languageId, int pageId, int pageSize)
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

            AllRaritiesPageQuery query = new AllRaritiesPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allRaritiesPageQueryHandler, query);

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
        /// Get rarity translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{rarityId}/{languageId}")]
        public async Task<IActionResult> GetRarityById(string languageId, int rarityId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            RarityByIdQuery query = new RarityByIdQuery()
            {
                LanguageId = languageId,
                Id = rarityId
            };

            ApiResponse response = await _dispatcher.Dispatch(_rarityByIdQueryHandler, query);

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