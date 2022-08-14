using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Repository.Infrastructure;
using System;
using CrossCutting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Service.Services;
using Domain.Interfaces.Repositories;
using Repository.Repositories;
using API.Middlewares;

namespace API
{
    public class Startup
    {
        public Startup()
        {
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Setup();
            services.SetupService();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            { 
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "",
                    Description = "",
                    TermsOfService = new Uri("https://github.com/serafim5511/"),
                    Contact = new OpenApiContact()
                    {
                        Name = "",
                        Email = "",
                        Url = new Uri("https://github.com/serafim5511/")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "",
                        Url = new Uri("https://github.com/serafim5511/")
                    }
                });
                //var arquivoSwagger = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var diretorio = Path.Combine(AppContext.BaseDirectory, arquivoSwagger);
                //c.IncludeXmlComments(diretorio);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseSwagger();

            app.UseSwaggerUI(ui => ui.SwaggerEndpoint("./v1/swagger.json", "Projeto EF CRUD"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
