using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrapController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllTrapsQueryHandler _allTrapsQueryHandler;
        private readonly AllTrapsPageQueryHandler _allTrapsPageQueryHandler;
        private readonly TrapByIdQueryHandler _trapByIdQueryHandler;

        public TrapController(UseCaseDispatcher dispatcher, AllTrapsQueryHandler allTrapsQueryHandler, AllTrapsPageQueryHandler allTrapsPageQueryHandler, TrapByIdQueryHandler trapByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allTrapsQueryHandler = allTrapsQueryHandler;
            _allTrapsPageQueryHandler = allTrapsPageQueryHandler;
            _trapByIdQueryHandler = trapByIdQueryHandler;
        }
        /// <summary>
        /// Get all trap types translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllTrapsTypes(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllTrapsQuery query = new AllTrapsQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allTrapsQueryHandler,query);

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
        /// Get paginated traps translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetTrapsPageById(string languageId, int pageId, int pageSize)
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

            AllTrapsPageQuery query = new AllTrapsPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allTrapsPageQueryHandler, query);

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
        /// Get trap type translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{trapTypeId}/{languageId}")]
        public async Task<IActionResult> GetTrapTypeById(string languageId, int trapTypeId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            TrapByIdQuery query = new TrapByIdQuery()
            {
                LanguageId = languageId,
                Id = trapTypeId
            };

            ApiResponse response = await _dispatcher.Dispatch(_trapByIdQueryHandler, query);

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