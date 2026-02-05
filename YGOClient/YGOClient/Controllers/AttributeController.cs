using Microsoft.AspNetCore.Mvc;
using YGOClient.Aggregator.Interfaces;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttributeController : ControllerBase
    {

        private readonly IAttributeAggregator _dispatcher;

        public AttributeController(IAttributeAggregator dispatcher)
        {
            _dispatcher = dispatcher;
        }
        /// <summary>
        /// Get all attributes translated by languageId
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}")]
        public async Task<IActionResult> GetAllAttributes(string languageId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AllAttributesQuery query = new AllAttributesQuery()
            {
                LanguageId = languageId
            };

            ApiResponse response = await _dispatcher.AllAttributesQuery(query);

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
        /// Get paginated attributes translated by languageId, page and size
        /// </summary>
        [HttpGet]
        [Route("all/{languageId}/page/{pageId}/size/{pageSize}")]
        public async Task<IActionResult> GetAttributesPageById(string languageId, int pageId, int pageSize)
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

            AllAttributesPageQuery query = new AllAttributesPageQuery()
            {
                LanguageId = languageId,
                PageId = pageId,
                PageSize = pageSize
            };

            ApiResponse response = await _dispatcher.AllAttributesPageQuery(query);

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
        /// Get attribute translated by languageId and typeId
        /// </summary>
        [HttpGet]
        [Route("{attributeId}/{languageId}")]
        public async Task<IActionResult> GetAttributeById(string languageId, int attributeId)
        {
            if (languageId == null)
            {
                return BadRequest();
            }

            AttributeByIdQuery query = new AttributeByIdQuery()
            {
                LanguageId = languageId,
                Id = attributeId
            };

            ApiResponse response = await _dispatcher.AttributeByIdQuery(query);

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