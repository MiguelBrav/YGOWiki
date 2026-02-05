using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpellController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllSpellsQueryHandler _allSpellsQueryHandler;
        private readonly AllSpellsPageQueryHandler _allSpellsPageQueryHandler;
        private readonly SpellByIdQueryHandler _spellByIdQueryHandler;

        public SpellController(UseCaseDispatcher dispatcher, AllSpellsQueryHandler allSpellsQueryHandler, AllSpellsPageQueryHandler allSpellsPageQueryHandler, SpellByIdQueryHandler spellByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allSpellsQueryHandler = allSpellsQueryHandler;
            _allSpellsPageQueryHandler = allSpellsPageQueryHandler;
            _spellByIdQueryHandler = spellByIdQueryHandler;
        }
        /// <summary>
        /// Get all spells types translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllSpellsTypes(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllSpellsQuery query = new AllSpellsQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allSpellsQueryHandler, query);

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
        /// Get paginated spells translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetSpellsPageById(string languageId, int pageId, int pageSize)
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

            AllSpellsPageQuery query = new AllSpellsPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allSpellsPageQueryHandler, query);

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
        /// Get spell type translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{spellTypeId}/{languageId}")]
        public async Task<IActionResult> GetSpellTypeById(string languageId, int spellTypeId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            SpellByIdQuery query = new SpellByIdQuery()
            {
                LanguageId = languageId,
                Id = spellTypeId
            };

            ApiResponse response = await _dispatcher.Dispatch(_spellByIdQueryHandler, query);

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