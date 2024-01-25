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
    public class SpellController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SpellController(IMediator mediator)
        {
            _mediator = mediator;
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