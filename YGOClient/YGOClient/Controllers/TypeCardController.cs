using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypeCardController : ControllerBase
    {

        private readonly UseCaseDispatcher _dispatcher;
        private readonly AllTypeCardsQueryHandler _allTypeCardsQueryHandler;
        private readonly AllTypeCardsPageQueryHandler _allTypeCardsPageQueryHandler;
        private readonly TypeCardByIdQueryHandler _typeCardByIdQueryHandler;

        public TypeCardController(UseCaseDispatcher dispatcher, AllTypeCardsQueryHandler allTypeCardsQueryHandler, AllTypeCardsPageQueryHandler allTypeCardsPageQueryHandler, TypeCardByIdQueryHandler typeCardByIdQueryHandler)
        {
            _dispatcher = dispatcher;
            _allTypeCardsQueryHandler = allTypeCardsQueryHandler;
            _allTypeCardsPageQueryHandler = allTypeCardsPageQueryHandler;
            _typeCardByIdQueryHandler = typeCardByIdQueryHandler;
        }
        /// <summary>
        /// Get all type cards translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllTypeCards(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllTypeCardsQuery query = new AllTypeCardsQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.Dispatch(_allTypeCardsQueryHandler,query);

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
        /// Get paginated type cards translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetTypeCardsPageById(string languageId, int pageId, int pageSize)
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

            AllTypeCardsPageQuery query = new AllTypeCardsPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.Dispatch(_allTypeCardsPageQueryHandler, query);

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
        /// Get type card translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{typeCardId}/{languageId}")]
        public async Task<IActionResult> GetTypeCardById(string languageId, int typeCardId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            TypeCardByIdQuery query = new TypeCardByIdQuery()
            {
                LanguageId = languageId,
                Id = typeCardId
            };

            ApiResponse response = await _dispatcher.Dispatch(_typeCardByIdQueryHandler, query);

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