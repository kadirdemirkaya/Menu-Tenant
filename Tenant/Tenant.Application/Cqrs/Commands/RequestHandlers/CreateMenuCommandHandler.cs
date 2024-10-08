﻿using EventBusDomain;
using Microsoft.Extensions.Logging;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models;
using Tenant.Application.Cqrs.Commands.Requests;
using Tenant.Application.Cqrs.Commands.Responses;

namespace Tenant.Application.Cqrs.Commands.RequestHandlers
{
    public class CreateMenuCommandHandler(IRepository<Menu, MenuId> _repository, IWorkContext _workContext, ILogger<CreateMenuCommandHandler> _logger) : IEventHandler<CreateMenuCommandRequest, CreateMenuCommandResponse>
    {
        public async Task<CreateMenuCommandResponse> Handle(CreateMenuCommandRequest @event)
        {
            int menuCount = await _repository.CountAsync(false);

            if (menuCount <= 10)
            {
                Menu menu = Menu.Create(MenuId.CreateUnique(), @event.CreateMenuModelDto.Name, @event.CreateMenuModelDto.Description, _workContext.Tenant?.CompanyName ?? null);
                menu.SetActive(false);

                Address address = Address.Create(@event.CreateMenuModelDto.Street, @event.CreateMenuModelDto.City, @event.CreateMenuModelDto.Country);

                menu.AddAddress(address);

                try
                {
                    bool createRes = await _repository.CreateAsync(menu);

                    createRes = await _repository.SaveCahangesAsync();

                    return createRes ? new(ApiResponseModel<bool>.CreateSuccess(true)) : new(ApiResponseModel<bool>.CreateFailure<bool>("A error accured while menu created"));
                }
                catch (Exception ex)
                {
                    return new(ApiResponseModel<bool>.CreateFailure<bool>("A error accured while menu created"));
                }
            }

            _logger.LogError("{DateTime} : More than 10 menus cannot be created !", DateTime.UtcNow);

            return new(ApiResponseModel<bool>.CreateFailure<bool>("More than 10 menus cannot be created"));
        }
    }
}
