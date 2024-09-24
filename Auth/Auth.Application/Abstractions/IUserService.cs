using Auth.Application.Dtos.User;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;

namespace Auth.Application.Abstractions
{
    public interface IUserService : IRepository<AppUser, AppUserId>
    {
        Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto);
    }
}
