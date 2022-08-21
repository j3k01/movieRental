using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using movieRental.Api.HealthChecks;
using movieRental.Data.Sql;
using movieRental.Data.Sql.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using movieRental.Api.Middlewares;
using movieRental.Api.BindingModels;
using FluentValidation.AspNetCore;
using FluentValidation;
using movieRental.IData.Client;
using movieRental.IServices.Client;
using movieRental.Services.Client;
using movieRental.Api.Validation;
using movieRental.Data.Sql.Client;
using Microsoft.AspNetCore.Cors;

namespace movieRental
{
    public class Startup
    {
        //Reprezentuje zestaw właściwości konfiguracyjnych aplikacji klucz / wartość. (np z pliku appsettings.json)
        public IConfiguration Configuration { get; }
        //private const string MySqlHealthCheckName = "mysql";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //metoda odpowiadająca za rejestrację serwisów w kontenerze IoC
        public void ConfigureServices(IServiceCollection services)
        {
           
            //rejestracja DbContextu, użycie providera MySQL i pobranie danych o bazie z appsettings.json
            services.AddDbContext<movieRentalDbContext>(options => options
                .UseMySQL(Configuration.GetConnectionString("movieRentalDbContext")));
            services.AddTransient<DatabaseSeed>();
            services.AddControllers().AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
                .AddFluentValidation();
            services.AddTransient<IValidator<EditClient>, Api.Validation.EditClientValidator>();
            services.AddTransient<IValidator<CreateClient>, CreateClientValidator>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientRepository, ClientRepository>();

            services.AddApiVersioning(o => { o.ReportApiVersions = true;
                o.UseApiBehavior = false;
            });


            //dodanie health checku i konfiguracja health checku dla MySqla
            //services.AddHealthChecks()
            //    .AddMySql(
            //        Configuration.GetConnectionString("movieRentalDbServer"),
            //        4,
            //        10,
            //        MySqlHealthCheckName);
            services.AddCors();
        }
         public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
         {
            app.UseCors(options => options.WithOrigins("http://localhost:8081")
                .AllowAnyHeader()
                .AllowAnyMethod());



            app.UseAuthentication();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //wystawienie pod adresem /healthy stanu healthchecków
            //app.UseHealthChecks("/healthy");

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<movieRentalDbContext>();
                var databaseSeed = serviceScope.ServiceProvider.GetRequiredService<DatabaseSeed>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                databaseSeed.Seed();
                //sprawdzenie czy health check się powiódł
                //var healthCheck = serviceScope.ServiceProvider.GetRequiredService<HealthCheckService>();
                //if (healthCheck.CheckHealthAsync().Result?.Entries[MySqlHealthCheckName].Status
                //== HealthCheckResult.Unhealthy().Status)
                //{
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                //databaseSeed.Seed();
                //}\

                app.UseMiddleware<ErrorHandlerMiddleware>();
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }

        }
    }
}
 