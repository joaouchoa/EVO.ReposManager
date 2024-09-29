using EVO.ReposManager.Application;
using EVO.ReposManager.Infrastructure;

namespace EVO.ReposManager.WebApi.Setup
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddReadApplication();
            services.AddQueryInfrastructure();

            return services;
        }
    }
}
