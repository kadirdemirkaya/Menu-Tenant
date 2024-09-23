using Shared.Application.Abstractions;
using Shared.Application.Models.Dtos.User;
using Shared.Domain.Aggregates.UserAggregate;

namespace Auth.Application.Abstractions
{
    public interface IUserService : IRepository<AppUser>
    {
        Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto);
    }
}
