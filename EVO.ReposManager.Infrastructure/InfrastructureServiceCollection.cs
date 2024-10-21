using EVO.ReposManager.Application.Contracts;
using EVO.ReposManager.Infrastructure.Config;
using EVO.ReposManager.Infrastructure.Context;
using EVO.ReposManager.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVO.ReposManager.Infrastructure
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddQueryInfrastructure(this IServiceCollection services) 
        {
            services.AddScoped<ReposManagerContext>();
            services.AddScoped<IReposReadRepository, ReposReadRepository>();
            services.AddScoped<IReposWriteRepository, ReposWriteRepository>(); 

            return services;
        }
    }
}
