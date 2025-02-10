using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Authentication.Domain;
using Authentication.Domain.Entities;
using Authentication.Infrastructure.Data;
using Authentication.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedService.Lib.DI;

namespace Authentication.Infrastructure.DI
{
    public static class ServiceContainer
    {
        // centralizes the configuration and lifetime management of services, promoting decoupling and improving maintainability

        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration) 
        {
            SharedServicesContainer.AddSharedServices<AuthenticationDbContext>(services, configuration);
            services.AddScoped<IAccountRepository<Account>, AccountRepository>();
            services.AddScoped<IUserRepository<User>, UserRepository>();
            services.AddScoped<IAccountService, AccountService>();
            return services; 
        }

        public static IApplicationBuilder AccountInfrastructurePolicy(this IApplicationBuilder app)
        {
            SharedServicesContainer.UseSharedPolicies(app);
            return app;
        }
    }
}
