using Auth.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using SecretManagement;
using Shared.Application.Models.Dtos.User;
using Shared.Application.Repository;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Infrastructure.Services
{
    public class UserService : Repository<AppUser>, IUserService<AppUser>
    {
        private DbContext _dbContext;
        private ISecretsManagerService _secretsManagerService;
        public UserService(DbContext context, ISecretsManagerService secretsManagerService) : base(context)
        {
            _dbContext = context;
            _secretsManagerService = secretsManagerService;
        }

        public async Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto)
        {
            AppUser user = AppUser.Create(AppUserId.CreateUnique(), userRegisterModelDto.userModelDto.username, userRegisterModelDto.userModelDto.email, userRegisterModelDto.userModelDto.password, userRegisterModelDto.userModelDto.phoneNumber);

            Company company = Company.Create(userRegisterModelDto.companyModelDto.name, userRegisterModelDto.companyModelDto.databaseName, user.Id);

            ConnectionPool connectionPool = null;

            user.AddCompany(company);



            if (userRegisterModelDto.connectionPoolModelDto.IsWantShared)
            {
                connectionPool = ConnectionPool.Create("postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresSharedHost), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresSharedPort), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresSharedDatabaseName), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresSharedUserName), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPostgresSharedPassword), company.Id);



            }
            else
            {
                //connectionPool = ConnectionPool.Create(); // mono db for user
            }

            await _dbContext.Set<AppUser>().AddAsync();

            company.AddConnectionPool(connectionPool);
        }
    }
}
