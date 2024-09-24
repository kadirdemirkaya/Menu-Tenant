using Auth.Application.Abstractions;
using Auth.Application.Dtos.User;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Infrastructure.Services
{
    public class UserService : Repository<AppUser, AppUserId>, IUserService, IRepository<AppUser, AppUserId>
    {
        private AuthDbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        private ISecretsManagerService _secretsManagerService;
        public UserService(AuthDbContext context, ISecretsManagerService secretsManagerService, ILogger<UserService> logger) : base(context)
        {
            _dbContext = context;
            _secretsManagerService = secretsManagerService;
            _logger = logger;
        }

        public async Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto)
        {
            if (await AnyAsync(u => u.Email == userRegisterModelDto.userModelDto.email, false, true))
            {
                _logger.LogError("User alread exists.");
                return false;
            }

            string tenantId = Guid.NewGuid().ToString();
            AppUser user = AppUser.Create(AppUserId.CreateUnique(), userRegisterModelDto.userModelDto.username, userRegisterModelDto.userModelDto.email, userRegisterModelDto.userModelDto.password, userRegisterModelDto.userModelDto.phoneNumber, tenantId);

            ConnectionPool connectionPool = null;

            string dbName = Shared.Infrastructure.Extensions.StringExtension.GenerateTemporaryDatabaseName();

            Company company = Company.Create(CompanyId.CreateUnique(), userRegisterModelDto.companyModelDto.name, userRegisterModelDto.connectionPoolModelDto.IsWantShared is true ? Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb : dbName, user.Id, tenantId);

            if (userRegisterModelDto.connectionPoolModelDto.IsWantShared)
                connectionPool = ConnectionPool.Create(ConnectionPoolId.CreateUnique(), "postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), company.Id, tenantId);
            else
                connectionPool = ConnectionPool.Create(ConnectionPoolId.CreateUnique(), "postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), dbName, await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), company.Id, tenantId);

            company.AddConnectionPool(connectionPool);

            user.AddCompany(company);

            if (await CreateAsync(user))
                return await SaveCahangesAsync();

            return false;
        }
    }
}
