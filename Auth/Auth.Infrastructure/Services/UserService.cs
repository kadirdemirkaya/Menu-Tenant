using Auth.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Application.Models.Dtos.User;
using Shared.Application.Repository;
using Shared.Domain.Aggregates.UserAggregate;
using Shared.Domain.Aggregates.UserAggregate.Entities;
using Shared.Domain.Aggregates.UserAggregate.ValueObjects;
using Shared.Domain.Models;

namespace Auth.Infrastructure.Services
{
    public class UserService : Repository<AppUser>, IUserService
    {
        private DbContext _dbContext;
        private readonly ILogger<UserService> _logger;
        private readonly IUserService _userService;
        private ISecretsManagerService _secretsManagerService;
        public UserService(DbContext context, ISecretsManagerService secretsManagerService, IUserService userService, ILogger<UserService> logger) : base(context)
        {
            _dbContext = context;
            _secretsManagerService = secretsManagerService;
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto)
        {
            AppUser user = AppUser.Create(AppUserId.CreateUnique(), userRegisterModelDto.userModelDto.username, userRegisterModelDto.userModelDto.email, userRegisterModelDto.userModelDto.password, userRegisterModelDto.userModelDto.phoneNumber);

            ConnectionPool connectionPool = null;

            Company company = Company.Create(userRegisterModelDto.companyModelDto.name, userRegisterModelDto.companyModelDto.databaseName, user.Id);

            AppUser? appUser = await _userService.GetAsync(null, false, u => u.Companies);

            if (!userRegisterModelDto.connectionPoolModelDto.IsWantShared)
                if (appUser is not null)
                    foreach (var companyItem in appUser.Companies)
                        if (companyItem.DatabaseName == userRegisterModelDto.companyModelDto.databaseName)
                        {
                            _logger.LogError("databasename already exists! Your can select other database names");
                            return false;
                        }

            company.AddConnectionPool(connectionPool);

            user.AddCompany(company);

            if (userRegisterModelDto.connectionPoolModelDto.IsWantShared)
                connectionPool = ConnectionPool.Create("postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_SharedDb), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), company.Id);
            else
                connectionPool = ConnectionPool.Create("postgresql", await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Host), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_Port), userRegisterModelDto.companyModelDto.databaseName, await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_User), await _secretsManagerService.GetSecretValueAsStringAsync(Constants.Secrets.DevelopmentPOSTGRES_POSTGRES_Password), company.Id);

            if (await _userService.AddAsync(user) != null)
                return await _userService.SaveChangesAsync();

            return false;
        }
    }
}
