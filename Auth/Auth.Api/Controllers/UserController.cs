using Auth.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Aggregates.UserAggregate;

namespace Auth.Api.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {

    }
}
