using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialMonsterController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllSpecialMonsterCardsQueryHandler _allSpecialMonsterCardsQueryHandler;
        private readonly AllSpecialMonsterCardsPageQueryHandler _allSpecialMonsterCardsPageQueryHandler;
        private readonly SpecialMonsterCardByIdQueryHandler _specialMonsterCardByIdQueryHandler;

        public SpecialMonsterController(UseCaseDispatcher dispatcher, AllSpecialMonsterCardsQueryHandler allSpecialMonsterCardsQueryHandler, AllSpecialMonsterCardsPageQueryHandler allSpecialMonsterCardsPageQueryHandler, SpecialMonsterCardByIdQueryHandler specialMonsterCardByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allSpecialMonsterCardsQueryHandler = allSpecialMonsterCardsQueryHandler;
            _allSpecialMonsterCardsPageQueryHandler = allSpecialMonsterCardsPageQueryHandler;
            _specialMonsterCardByIdQueryHandler = specialMonsterCardByIdQueryHandler;
        }
        /// <summary>
        /// Get all special monster types translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllSpecialMonsterTypes(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllSpecialMonsterCardsQuery query = new AllSpecialMonsterCardsQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allSpecialMonsterCardsQueryHandler, query);

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
        /// Get paginated special monsters translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetSpecialMonstersPageById(string languageId, int pageId, int pageSize)
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

            AllSpecialMonsterCardsPageQuery query = new AllSpecialMonsterCardsPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allSpecialMonsterCardsPageQueryHandler, query);

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
        /// Get special monster type translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{specialTypeId}/{languageId}")]
        public async Task<IActionResult> GetSpecialMonsterTypeById(string languageId, int specialTypeId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            SpecialMonsterCardByIdQuery query = new SpecialMonsterCardByIdQuery()
            {
                LanguageId = languageId,
                Id = specialTypeId
            };

            ApiResponse response = await _dispatcher.Dispatch(_specialMonsterCardByIdQueryHandler, query);

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