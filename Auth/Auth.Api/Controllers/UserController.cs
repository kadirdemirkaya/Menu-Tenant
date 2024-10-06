using Auth.Application.Cqrs.Commands.Requests;
using Auth.Application.Cqrs.Commands.Responses;
using Auth.Application.Dtos.User;
using EventBusDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    [Authorize]
    public class UserController(EventBus _eventBus) : BaseController
    {
        [HttpDelete]
        [Route("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromHeader] Guid id)
        {
            DeleteUserCommandRequest request = new(id);
            DeleteUserCommandResponse? response = await _eventBus.PublishAsync(request) as DeleteUserCommandResponse;

            return response.ApiResponseModel.Success is true ? Ok() : BadRequest(response.ApiResponseModel);
        }

        [HttpPut]
        [Route("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateModelDto userUpdateModelDto)
        {
            UpdateUserCommandRequest request = new(userUpdateModelDto);
            UpdateUserCommandResponse? response = await _eventBus.PublishAsync(request) as UpdateUserCommandResponse;

            return response.ApiResponseModel.Success is true ? Ok() : BadRequest(response.ApiResponseModel);
        }
    }
}
