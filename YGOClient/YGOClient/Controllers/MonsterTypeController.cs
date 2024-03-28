using Grpc.Net.Client;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerYGO;
using System.Text.Json;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using static System.Net.WebRequestMethods;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonsterTypeController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MonsterTypeController(IMediator mediator)
        {
            _mediator = mediator;
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

            ApiResponse response = await _mediator.Send(query);

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

            ApiResponse response = await _mediator.Send(query);

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

            ApiResponse response = await _mediator.Send(query);

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