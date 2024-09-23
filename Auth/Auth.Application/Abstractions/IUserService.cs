using Shared.Application.Models.Dtos.User;
using Shared.Domain.Aggregates.UserAggregate;

namespace Auth.Application.Abstractions
{
    public interface IUserService<TEntity> : IUserRepository<TEntity>
        where TEntity : AppUser
    {
        Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto);
    }
}
