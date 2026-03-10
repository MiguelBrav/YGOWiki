using Microsoft.AspNetCore.Mvc;
using UseCaseCore.UseCases;
using YGOClient.DTO.APIResponse;
using YGOClient.Queries;
using YGOClient.QueriesHandler;

namespace YGOClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UtilsController : ControllerBase
    {
        private readonly UseCaseDispatcher _dispatcher;
        private readonly HealthCheckQueryHandler _healthCheckQueryHandler;

        public UtilsController(UseCaseDispatcher dispatcher, HealthCheckQueryHandler healthCheckQueryHandler)
        {
            _dispatcher = dispatcher;
            _healthCheckQueryHandler = healthCheckQueryHandler;
        }

        [HttpGet]
        [Route("healthz")]
        public async Task<IActionResult> Healthz()
        {
            ApiResponse response = await _dispatcher.Dispatch(_healthCheckQueryHandler, new HealthCheckQuery());

            if (response.StatusCode == 204)
            {
                return StatusCode(response.StatusCode);
            }

            if (response.Response == null || response.Response is false)
            {
                return StatusCode(response.StatusCode, response.ResponseMessage);
            }

            return StatusCode(response.StatusCode, response.ResponseMessage);
        }
    }
}
