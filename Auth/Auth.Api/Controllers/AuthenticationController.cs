using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using Auth.Application.Dtos.User;
using EventBusDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Filters;
using Shared.Domain.Models;
using Shared.Domain.Models.User;

namespace Auth.Api.Controllers
{
    [AllowAnonymous]
    public class AuthenticationController(EventBus _eventBus) : BaseController
    {
        [HttpPost]
        [Route("Auth/Register")]
        [ServiceFilter(typeof(HashPasswordActionFilter))]
        public async Task<ActionResult<ApiResponseModel<bool>>> UserRegister([FromBody] UserRegisterModelDto userRegisterModelDto)
        {
            UserRegisterCommandRequest request = new(userRegisterModelDto);
            UserRegisterCommandResponse? response = await _eventBus.PublishAsync(request) as UserRegisterCommandResponse;

            return Ok(response.ApiResponseModel);
        }

        [HttpPost]
        [Route("Auth/Login")]
        [ServiceFilter(typeof(HashPasswordActionFilter))]
        public async Task<ActionResult<ApiResponseModel<UserLoginModel>>> UserLogin([FromBody] UserLoginModelDto userLoginModelDto)
        {
            UserLoginCommandRequest request = new(userLoginModelDto);
            UserLoginCommandResponse? response = await _eventBus.PublishAsync(request) as UserLoginCommandResponse;

            return Ok(response.ApiResponseModel);
        }
    }
}
