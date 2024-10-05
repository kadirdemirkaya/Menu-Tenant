using Base.Caching;
using Base.Caching.Key;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecretManagement;
using Shared.Application.Abstractions;
using Shared.Domain.Aggregates.MenuAggregate.Entities;
using Shared.Domain.Aggregates.MenuAggregate.ValueObjects;
using Shared.Domain.Aggregates.ProductAggregate;
using Shared.Domain.Models.ConnectionPools;
using Shared.Infrastructure;
using Shared.Stream;
using Tenant.Application.Abstractions;
using Tenant.Infrastructure.Data;
using Tenant.Infrastructure.Repository;
using Tenant.Infrastructure.Seeds;
using Tenant.Infrastructure.Services;

namespace Tenant.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection TenantInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPollyPolicies();

            services.SeqRegistration(configuration);

            services.SecretManagementRegistration();

            services.AddRedisRegistration(configuration);

            services.ServiceRegistration();

            services.CachingRegistration(configuration);

            services.AddDatabase();

            services.AddServices();

            services.AddStreamEvent();

            services.ApplySeeds(sp =>
            {
                using (var context = sp.GetRequiredService<MenuDbContext>())
                {
                    context.SaveChanges();
                }
            });

            return services;
        }

        private static IServiceCollection ApplySeeds(this IServiceCollection services, Action<IServiceProvider> seedAction)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                SeedData seedData;

                var dbContext = serviceProvider.GetRequiredService<MenuDbContext>();
                var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();
                var secretManagement = serviceProvider.GetRequiredService<ISecretsManagerService>();
                var cacheManager = serviceProvider.GetRequiredService<ICacheManager>();
                var polyService = serviceProvider.GetRequiredService<IPollyPolicyService>();

                seedData = new(dbContext, logger, secretManagement, cacheManager, polyService);

                seedData.MigApplyFromCache().SeedDataApply().GetAwaiter().GetResult();

                seedAction(serviceProvider);
            }

            return services;
        }

        private static IServiceCollection AddStreamEvent(this IServiceCollection services)
        {
            services.AddStreamBus(AssemblyReference.Assembly);

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<MenuDbContext>((sp, options) =>
            {
                CacheKey cacheKey = CacheKey.Create("connectionwithcompany");

                List<ConnectionPoolWithCompanyModel> conCompModels = new();
                bool isConfigure = false;
                var workContext = sp.GetRequiredService<IWorkContext>();
                var cacheManager = sp.GetRequiredService<ICacheManager>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

                conCompModels = cacheManager.Get<List<ConnectionPoolWithCompanyModel>>(cacheKey,
                      () => Task.FromResult<List<ConnectionPoolWithCompanyModel>>(null).GetAwaiter().GetResult()
                );

                string? companyName = httpContextAccessor.HttpContext?.Items["CompanyName"]?.ToString();

                // TODO : companyName is empty and if not come tenantid |||| we are didn't matching to urls 
                // TODO : shareddb tenantId value not equals connectionpools tenantId value !!!
                if (conCompModels != null)
                    if (conCompModels.Count() != 0 && companyName != null)
                    {
                        foreach (var conCompModel in conCompModels)
                        {
                            if (conCompModel.CompanyName == companyName)
                            {
                                options.UseNpgsql(conCompModel.DbUrls);
                                isConfigure = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var conCompModel in conCompModels)
                        {
                            if (workContext.Tenant?.TenantId is null)
                                return;
                            else if (workContext.Tenant.TenantId == conCompModel.TenantId)
                            {
                                options.UseNpgsql(conCompModel.DbUrls);
                                isConfigure = true;
                                break;
                            }
                        }
                    }

                if (!isConfigure)
                    options.UseNpgsql("Server=localhost;port=5432;Database=shareddb;User Id=admin;Password=passw00rd");
            });

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Product, ProductId>, Repository<Product, ProductId>>();

            services.AddScoped<IRepository<Menu, MenuId>, Repository<Menu, MenuId>>();

            services.AddScoped<IImageService, ImageService>();

            return services;
        }

        public static WebApplication AuthInfrastructureWebApplicationRegistration(this WebApplication app)
        {
            app.MiddlewareRegistration();

            return app;
        }
    }
}
