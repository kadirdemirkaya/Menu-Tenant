using EventBusDomain;
using Tenant.Application.Cqrs.Queries.Requests;
using Tenant.Application.Cqrs.Queries.Responses;

namespace Tenant.Application.Cqrs.Queries.RequestHandlers
{
    public class GetActiveMenuOfCompanyQueryHandler : IEventHandler<GetActiveMenuOfCompanyQueryRequest, GetActiveMenuOfCompanyQueryResponse>
    {
        public async Task<GetActiveMenuOfCompanyQueryResponse> Handle(GetActiveMenuOfCompanyQueryRequest @event)
        {
            throw new NotImplementedException();
        }
    }
}
