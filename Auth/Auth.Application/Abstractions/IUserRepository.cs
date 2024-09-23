using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;

namespace Auth.Application.Abstractions
{
    public interface IUserRepository<TEntity> : IRepository<TEntity>
        where TEntity : AppUser
    {
    }
}
