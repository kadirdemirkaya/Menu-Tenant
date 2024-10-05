using EventBusDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;
using Tenant.Application.Cqrs.Queries.Requests;
using Tenant.Application.Cqrs.Queries.Responses;
using Tenant.Application.Dtos;

namespace Tenant.Api.Controllers
{
    [Authorize]
    public class MenuController(EventBus _eventBus) : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("/company/{companyName}")]
        public async Task<ActionResult> GetActiveMenuOfCompany()
        {
            GetActiveMenuOfCompanyQueryRequest request = new();
            GetActiveMenuOfCompanyQueryResponse response = await _eventBus.PublishAsync(request) as GetActiveMenuOfCompanyQueryResponse;

            return Ok();
        }

        [HttpGet]
        [Route("/company/getallmenu")]
        public async Task<ActionResult> GetAllMenu()
        {
            GetAllMenuQueryRequest request = new();
            GetAllMenuQueryResponse response = await _eventBus.PublishAsync(request) as GetAllMenuQueryResponse;

            return Ok(response.ApiResponseModel);
        }

        [HttpPost]
        [Route("/compnay/createmenu")]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuModelDto createMenuModelDto)
        {
            CreateMenuCommandRequest request = new(createMenuModelDto);
            CreateMenuCommandResponse response = await _eventBus.PublishAsync(request) as CreateMenuCommandResponse;

            return response.ApiResponseModel.Success ? Ok() : BadRequest(response.ApiResponseModel.Message);
        }

        [HttpPut]
        [Route("/company/activetemenu")]
        public async Task<IActionResult> ActiveteMenu([FromHeader] Guid menuId)
        {
            ActiveteMenuCommandRequest request = new(menuId);
            ActiveteMenuCommandResponse response = await _eventBus.PublishAsync(request) as ActiveteMenuCommandResponse;

            return response.ApiResponseModel.Success ? Ok() : BadRequest(response.ApiResponseModel.Message);
        }

        [HttpPut]
        [Route("/company/unactivitemenu")]
        public async Task<IActionResult> UnactiviteMenu([FromHeader] Guid menuId)
        {
            UnactiviteMenuCommandRequest request = new(menuId);
            UnactiviteMenuCommandResponse response = await _eventBus.PublishAsync(request) as UnactiviteMenuCommandResponse;

            return response.ApiResponseModel.Success ? Ok() : BadRequest(response.ApiResponseModel.Message);
        }
    }
}
