using Auth.Application.Dtos.User;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Application.Abstractions
{
    public interface IUserService : IRepository<AppUser, AppUserId>
    {
        Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto);

        Task<Token?> UserLoginAsync(UserLoginModelDto userLoginModelDto);
    }
}
