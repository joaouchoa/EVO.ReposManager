using EVO.ReposManager.Infrastructure.Config;
using EVO.ReposManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace EVO.ReposManager.WebApi.Setup
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDependencyInjection();
            services.Configure<GitHubSettings>(configuration.GetSection("GitHub"));
            services.AddDbContext<ReposManagerContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
