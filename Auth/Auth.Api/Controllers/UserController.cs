using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;

namespace Auth.Api.Controllers
{
    [Authorize]
    public class UserController(AuthDbContext _context) : BaseController
    {
        [HttpGet]
        [Route("GetUserWithTenantId")]
        public async Task<IActionResult> GetUserWithTenantId()
        {
            var userWithTenantId = await _context.Users.FirstOrDefaultAsync();
            return Ok(userWithTenantId);
        }

        [HttpGet]
        [Route("GetUserWithTenantId2")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserWithTenantId2()
        {
            var userWithTenantId = await _context.Users.FirstOrDefaultAsync();
            return Ok(userWithTenantId);
        }
    }
}
