using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonsterTypeController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllMonsterTypesQueryHandler _allMonsterTypesQueryHandler;
        private readonly AllMonsterTypesPageQueryHandler _allMonsterTypesPageQueryHandler;
        private readonly MonsterTypeByIdQueryHandler _monsterTypeByIdQueryHandler;

        public MonsterTypeController(UseCaseDispatcher dispatcher, AllMonsterTypesQueryHandler allMonsterTypesQueryHandler, AllMonsterTypesPageQueryHandler allMonsterTypesPageQueryHandler, MonsterTypeByIdQueryHandler monsterTypeByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allMonsterTypesQueryHandler = allMonsterTypesQueryHandler;
            _allMonsterTypesPageQueryHandler = allMonsterTypesPageQueryHandler;
            _monsterTypeByIdQueryHandler = monsterTypeByIdQueryHandler;
        }
        /// <summary>
        /// Get all monster types translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllMonsterTypes(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllMonsterTypesQuery query = new AllMonsterTypesQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allMonsterTypesQueryHandler, query);

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
        /// Get paginated monster types translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetMonsterTypesPageById(string languageId, int pageId, int pageSize)
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

            AllMonsterTypesPageQuery query = new AllMonsterTypesPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allMonsterTypesPageQueryHandler, query);

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
        /// Get monster type translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{monsterTypeId}/{languageId}")]
        public async Task<IActionResult> GetMonsterTypeById(string languageId, int monsterTypeId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            MonsterTypeByIdQuery query = new MonsterTypeByIdQuery()
            {
                LanguageId = languageId,
                Id = monsterTypeId
            };

            ApiResponse response = await _dispatcher.Dispatch(_monsterTypeByIdQueryHandler, query);

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