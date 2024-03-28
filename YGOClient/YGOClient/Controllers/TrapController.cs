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
    public class TrapController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TrapController(IMediator mediator)
        {
            _mediator = mediator;
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