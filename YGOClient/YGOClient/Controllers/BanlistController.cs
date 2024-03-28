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
    public class BanlistController : ControllerBase
    {

        private readonly IMediator _mediator;

        public BanlistController(IMediator mediator)
        {
            _mediator = mediator;
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