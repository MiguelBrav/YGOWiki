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
    public class TypeCardController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TypeCardController(IMediator mediator)
        {
            _mediator = mediator;
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