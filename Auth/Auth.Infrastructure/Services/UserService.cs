using Auth.Application.Abstractions;
using Auth.Application.Dtos.User;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IJwtTokenService _jwtTokenService;
        private ISecretsManagerService _secretsManagerService;
        private readonly IRepository<Company, CompanyId> _companyRepository;
        private readonly IRepository<ConnectionPool, ConnectionPoolId> _connectionRepository;

        public UserService(AuthDbContext context, ISecretsManagerService secretsManagerService, ILogger<UserService> logger, IJwtTokenService jwtTokenService, IRepository<ConnectionPool, ConnectionPoolId> connectionRepository, IRepository<Company, CompanyId> companyRepository) : base(context)
        {
            _dbContext = context;
            _secretsManagerService = secretsManagerService;
            _logger = logger;
            _jwtTokenService = jwtTokenService;
            _connectionRepository = connectionRepository;
            _companyRepository = companyRepository;
        }

        public async Task<bool> UserRegisterAsync(UserRegisterModelDto userRegisterModelDto)
        {
            if (await AnyAsync(u => u.Email == userRegisterModelDto.userModelDto.email, false, true)) // !!!
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

        public async Task<Token?> UserLoginAsync(UserLoginModelDto userLoginModelDto)
        {
            if (userLoginModelDto.CompanyName is null) // user have one companies
            {
                AppUser user = await GetAsync(u => u.Email == userLoginModelDto.Email && u.Password == userLoginModelDto.Password, false, true, u => u.Companies); // !!!

                if (user is null)
                {
                    _logger.LogError("User is not exists.");
                    return null;
                }

                TenantModel tenantModel = new()
                {
                    TenantId = user.TenantId,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DatabaseName = user.Companies.FirstOrDefault().DatabaseName
                };

                ConnectionPool connectionPool = await _connectionRepository.GetAsync(cp => cp.TenantId == user.TenantId, false, true, null); // !!!

                tenantModel.Name = connectionPool.Name;
                tenantModel.Host = connectionPool.Host;
                tenantModel.Port = connectionPool.Port;
                tenantModel.Password = connectionPool.Password;

                Token token = _jwtTokenService.GenerateToken(tenantModel);

                return token;
            }
            else // user have one of more companies
            {

                AppUser user = await GetAsync(u => u.Email == userLoginModelDto.Email && u.Password == userLoginModelDto.Password, false, true); // !!!

                if (user is null)
                {
                    _logger.LogError("User is not exists.");
                    return null;
                }

                Company company = await _companyRepository.GetAsync(c => c.Name == userLoginModelDto.CompanyName && c.TenantId == user.TenantId, false, true, c => c.ConnectionPool); // !!!


                TenantModel tenantModel = new()
                {
                    TenantId = user.TenantId,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    DatabaseName = user.Companies.FirstOrDefault().DatabaseName,
                    Name = company.ConnectionPool.Name,
                    Host = company.ConnectionPool.Host,
                    Port = company.ConnectionPool.Port,
                    Password = company.ConnectionPool.Password,
                };

                Token token = _jwtTokenService.GenerateToken(tenantModel);

                return token;
            }
        }
    }
}
