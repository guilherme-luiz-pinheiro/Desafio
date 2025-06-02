using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Desafio.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Desafio.Application.Interfaces;
using Desafio.Application;
using Desafio.Persistence;
using Desafio.Persistence.Interfaces;
using Desafio.API.Hubs;

namespace Desafio.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Batteries.Init();

            // Registro das dependênciass
            services.AddScoped<IMachineService, MachineService>();
            services.AddScoped<IGeralPersistence, GeralPersistence>();
            services.AddScoped<IMachinePersistence, MachinePersistence>();
            services.AddScoped<ITelemetryService, TelemetryService>();
            services.AddScoped<ITelemetryPersistence, TelemetryPersistence>(); // ✅ ESSA LINHA

            services.AddDbContext<DesafioContext>(
                context => context.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddControllers(options =>
            {
                options.OutputFormatters.Insert(0, new Microsoft.AspNetCore.Mvc.Formatters.StringOutputFormatter());
            });
            services.AddSignalR();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", builder =>
                {
                    builder.WithOrigins("http://localhost:4200") // Use http se Angular não usar https
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio.API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // ✅ Coloque o CORS aqui
            app.UseCors("AllowAngularApp");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TelemetryHub>("/ws/telemetry");
            });
        }

    }
}
