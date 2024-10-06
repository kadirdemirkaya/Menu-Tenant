using EventBusDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models;
using Tenant.Application.Cqrs.Queries.Requests;
using Tenant.Application.Cqrs.Queries.Responses;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Queries.RequestHandlers
{
    public class GetActiveMenuOfCompanyQueryHandler(IRepository<Menu, MenuId> _repository, IHttpContextAccessor _httpContextAccessor, IWorkContext _workContext) : IEventHandler<GetActiveMenuOfCompanyQueryRequest, GetActiveMenuOfCompanyQueryResponse>
    {
        public async Task<GetActiveMenuOfCompanyQueryResponse> Handle(GetActiveMenuOfCompanyQueryRequest @event)
        {
            int page = int.Parse(_httpContextAccessor.HttpContext?.Items["page"]?.ToString() ?? "1");
            int size = int.Parse(_httpContextAccessor.HttpContext?.Items["size"]?.ToString() ?? "10");
            string companyName = _workContext.CompanyName;

            Menu? menu = await _repository.GetAsync(m => m.IsActive == true && !m.IsDeleted && m.WebUrl == companyName, false, true, m => m.Products);

            AllMenuModelDto allMenuModelDto = new();
            allMenuModelDto.MenuMapper(menu.Id.Id, menu.Name, menu.IsActive, menu.Address, menu.Products, menu.Description, menu.WebUrl);

            return new(ApiResponseModel<AllMenuModelDto>.CreateSuccess(allMenuModelDto));
        }
    }
}
