using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Infrastructure;
using Repository.Repositories;
using System;

namespace CrossCutting
{
    public static class DatabaseConfig
    {
        public static void Setup(this IServiceCollection services)
        {

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}