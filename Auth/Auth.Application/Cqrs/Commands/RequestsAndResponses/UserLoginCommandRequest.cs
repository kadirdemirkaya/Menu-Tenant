﻿using Auth.Application.Dtos.User;
using EventBusDomain;

namespace Auth.Application.Cqrs.Commands.RequestsAndResponses
{
    public class UserLoginCommandRequest : IEventRequest
    {
        public UserLoginModelDto userLoginModelDto { get; set; }

        public UserLoginCommandRequest(UserLoginModelDto userLoginModelDto)
        {
            this.userLoginModelDto = userLoginModelDto;
        }
    }
}
