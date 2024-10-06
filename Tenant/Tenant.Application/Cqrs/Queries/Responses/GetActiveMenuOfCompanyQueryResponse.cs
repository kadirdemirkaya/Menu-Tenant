using EventBusDomain;
using Shared.Domain.Models;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Queries.Responses
{
    public class GetActiveMenuOfCompanyQueryResponse : IEventResponse
    {
        public ApiResponseModel<AllMenuModelDto> ApiResponseModel { get; set; }

        public GetActiveMenuOfCompanyQueryResponse(ApiResponseModel<AllMenuModelDto> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
