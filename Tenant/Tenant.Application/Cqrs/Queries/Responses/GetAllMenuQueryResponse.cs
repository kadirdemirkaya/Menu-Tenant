using EventBusDomain;
using Shared.Domain.Models;
using Tenant.Application.Dtos;

namespace Tenant.Application.Cqrs.Queries.Responses
{
    public class GetAllMenuQueryResponse : IEventResponse
    {
        public ApiResponseModel<List<AllMenuModelDto>> ApiResponseModel { get; set; }

        public GetAllMenuQueryResponse(ApiResponseModel<List<AllMenuModelDto>> apiResponseModel)
        {
            ApiResponseModel = apiResponseModel;
        }
    }
}
