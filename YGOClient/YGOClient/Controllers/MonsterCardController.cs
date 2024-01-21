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
    public class MonsterCardController : ControllerBase
    {

        private readonly IMediator _mediator;

        public MonsterCardController(IMediator mediator)
        {
            _mediator = mediator;
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

            AllTypeCardsQuery query = new AllTypeCardsQuery()
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

            TypeCardByIdQuery query = new TypeCardByIdQuery()
            {
                LanguageId = languageId,
                Id = monsterCardId
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