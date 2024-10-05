using EventBusDomain;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Tenant.Application.Cqrs.Queries.Requests;
using Tenant.Application.Cqrs.Queries.Responses;

namespace Tenant.Application.Cqrs.Queries.RequestHandlers
{
    public class GetAllMenuQueryHandler(IRepository<Menu, MenuId> _repository) : IEventHandler<GetAllMenuQueryRequest, GetAllMenuQueryResponse>
    {
        public async Task<GetAllMenuQueryResponse> Handle(GetAllMenuQueryRequest @event)
        {
            //List<Menu>? menus = await _repository.GetAllAsync(m => m.IsActive == true && m.IsActive == false, false, false, m => m.Products);

            return new(default);
        }
    }
}
