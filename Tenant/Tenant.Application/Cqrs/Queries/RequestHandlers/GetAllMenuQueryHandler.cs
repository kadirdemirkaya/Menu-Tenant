using EventBusDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.Application.Abstractions;
using Shared.Application.Services;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models;
using Tenant.Application.Cqrs.Queries.Requests;
using Tenant.Application.Cqrs.Queries.Responses;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Queries.RequestHandlers
{
    public class GetAllMenuQueryHandler(IRepository<Menu, MenuId> _repository, IHttpContextAccessor _httpContextAccessor) : IEventHandler<GetAllMenuQueryRequest, GetAllMenuQueryResponse>
    {
        public async Task<GetAllMenuQueryResponse> Handle(GetAllMenuQueryRequest @event)
        {
            int page = int.Parse(_httpContextAccessor.HttpContext?.Items["page"]?.ToString() ?? "1");
            int size = int.Parse(_httpContextAccessor.HttpContext?.Items["size"]?.ToString() ?? "10");

            PaginatedList<Menu> menus = await _repository.GetAllAsync(m => !m.IsDeleted, false, page, size, false, m => m.Products);

            List<AllMenuModelDto> allMenuModelDtos = new();

            foreach (var menu in menus)
            {
                AllMenuModelDto allMenuModelDto = new();
                allMenuModelDtos.Add(allMenuModelDto.MenuMapper(menu.Id.Id, menu.Name, menu.IsActive, menu.Address, menu.Products, menu.Description, menu.WebUrl));
            }

            return new(ApiResponseModel<AllMenuModelDto>.CreateSuccess(allMenuModelDtos));
        }
    }
}
