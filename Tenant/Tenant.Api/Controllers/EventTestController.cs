using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Stream;
using Tenant.Api.Events;

namespace Tenant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventTestController : ControllerBase
    {
        private readonly RedisStreamService _redisStreamService;

        public EventTestController(RedisStreamService redisStreamService)
        {
            _redisStreamService = redisStreamService;
        }

        [HttpPost]
        public async Task<IActionResult> PostEvent()
        {
            await _redisStreamService.PublishEventAsync(new ConnectionPoolUpdateStreamEvent() { Message = "api test anpfnasf" }, StreamEnum.AuthApi);

            return Ok();
        }
    }
}
