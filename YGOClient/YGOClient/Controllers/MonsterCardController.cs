using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonsterCardController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllMonsterCardsQueryHandler _allMonsterCardsQueryHandler;
        private readonly AllMonsterCardsPageQueryHandler _allMonsterCardsPageQueryHandler;
        private readonly MonsterCardByIdQueryHandler _monsterCardByIdQueryHandler;

        public MonsterCardController(UseCaseDispatcher dispatcher, AllMonsterCardsQueryHandler allMonsterCardsQueryHandler, AllMonsterCardsPageQueryHandler allMonsterCardsPageQueryHandler, MonsterCardByIdQueryHandler monsterCardByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allMonsterCardsQueryHandler = allMonsterCardsQueryHandler;
            _allMonsterCardsPageQueryHandler = allMonsterCardsPageQueryHandler;
            _monsterCardByIdQueryHandler = monsterCardByIdQueryHandler;
        }
        /// <summary>
        /// Get all monster card types translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllMonsterCards(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllMonsterCardsQuery query = new AllMonsterCardsQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allMonsterCardsQueryHandler, query);

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
        /// Get paginated monster card types translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetMonsterCardsPageById(string languageId, int pageId, int pageSize)
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

            AllMonsterCardsPageQuery query = new AllMonsterCardsPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allMonsterCardsPageQueryHandler, query);

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
        /// Get monster card type translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{monsterCardId}/{languageId}")]
        public async Task<IActionResult> GetMonsterCardById(string languageId, int monsterCardId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            MonsterCardByIdQuery query = new MonsterCardByIdQuery()
            {
                LanguageId = languageId,
                Id = monsterCardId
            };

            ApiResponse response = await _dispatcher.Dispatch(_monsterCardByIdQueryHandler, query);

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