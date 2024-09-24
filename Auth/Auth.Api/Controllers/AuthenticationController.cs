using Auth.Application.Cqrs.Commands.RequestsAndResponses;
using Auth.Application.Dtos.User;
using EventBusDomain;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Filters;
using Shared.Domain.Models;

namespace Auth.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly EventBus _eventBus;

        public AuthenticationController(EventBus eventBus)
        {
            _eventBus = eventBus;
        }

        [HttpPost]
        [Route("Auth/Register")]
        [ServiceFilter(typeof(HashPasswordActionFilter))]
        public async Task<ActionResult<ApiResponseModel<bool>>> UserRegister([FromBody] UserRegisterModelDto userRegisterModelDto)
        {
            UserRegisterCommandRequest request = new(userRegisterModelDto);
            UserRegisterCommandResponse response = await _eventBus.PublishAsync(request) as UserRegisterCommandResponse;

            return Ok(response.ApiResponseModel);
        }

        [HttpPost]
        [Route("Auth/Login")]
        [ServiceFilter(typeof(HashPasswordActionFilter))]
        public async Task<ActionResult<bool>> UserLogin()
        {
            UserLoginCommandRequest request = new();
            UserLoginCommandResponse response = await _eventBus.PublishAsync(request) as UserLoginCommandResponse;

            return Ok(response);
        }
    }
}
