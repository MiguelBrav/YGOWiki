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
    public class SpecialMonsterController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SpecialMonsterController(IMediator mediator)
        {
            _mediator = mediator;
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