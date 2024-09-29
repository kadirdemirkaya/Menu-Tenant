using EventBusDomain;
using Microsoft.Extensions.Logging;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;

namespace Tenant.Application.Cqrs.Commands.RequestHandlers
{
    public class UnactiviteMenuCommandHandler(IRepository<Menu, MenuId> _repository, ILogger<UnactiviteMenuCommandHandler> _logger) : IEventHandler<UnactiviteMenuCommandRequest, UnactiviteMenuCommandResponse>
    {
        public async Task<UnactiviteMenuCommandResponse> Handle(UnactiviteMenuCommandRequest @event)
        {
            List<Menu> menus = await _repository.GetAllAsync();

            if (menus.Count() <= 10)
            {
                int menuActiveCount = menus.Count(m => m.IsActive == true);

                if (menuActiveCount == 1)
                {
                    Menu menu = menus.First(m => m.Id == @event.MenuId);

                    if (menu is not null)
                    {
                        menu.SetActive(false);
                        menu.SetUpdatedDateUTC(DateTime.UtcNow);
                        await _repository.SaveCahangesAsync();
                        return new(ApiResponseModel<bool>.CreateSuccess(true));
                    }
                    else
                    {
                        _logger.LogError("{DateTime} : No menu to activate was found ! ", DateTime.UtcNow);
                        return new(ApiResponseModel<bool>.CreateFailure<bool>("No menu to activate was found !"));
                    }
                }
                else
                {
                    _logger.LogError("{DateTime} : Already not exists active menu ! ", DateTime.UtcNow);
                    return new(ApiResponseModel<bool>.CreateFailure<bool>("Already not exists active menu"));
                }
            }
            else
            {
                _logger.LogError("{DateTime} : Menu gotta not create then more 10  !", DateTime.UtcNow);
                return new(ApiResponseModel<bool>.CreateFailure<bool>("Menu gotta not create then more 10 !"));
            }

            return new(default);
        }
    }
}
