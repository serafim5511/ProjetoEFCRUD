using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting
{
    public static class ServiceConfig
    {
        public static void SetupService(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
