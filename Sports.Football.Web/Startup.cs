using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sports.Football.Core.Components;
using Sports.Football.Core.ServiceClient;
using Sports.Football.Core.ServiceClient.Mappers;
using Sports.Football.Data;
using Sports.Football.Repositories.Implementations;
using Sports.Football.Repositories.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace Sports.Football.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            SetupSwagger(services);
            RegisterCoreComponents(services);
            RegisterRepositories(services);
            SetupDb(services);
        }

        private void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Football Data Import API",
                    Description = "Meant to import data from from some API",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Javier Capello",
                        Email = "javier.h.capello@gmail.com",
                        Url = "https://www.linkedin.com/in/javier-h-capello/"
                    }
                });
            });
        }

        public void RegisterCoreComponents(IServiceCollection services)
        {
            services.AddScoped<IServiceClient, FootballHttpServiceClient>();

            services.AddHttpClient<IServiceClient, FootballHttpServiceClient>(client =>
            {
                client.BaseAddress = new Uri(Configuration["HttpServiceClient:Football:BaseAddress"]);
                client.DefaultRequestHeaders.Add("x-auth-token", Configuration["HttpServiceClient:Football:Token"]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<ILogManager, TracerLogManager>();

            services.AddSingleton<IModelMapper, AutoModelMapper>();

            services.AddScoped<IFootballClient, FootballClient>();

            services.AddScoped<IFootballManager, FootballManager>();

            services.AddScoped<IComponentsProvider, DefaultComponentsProvider>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services
                .AddScoped<ICompetitionRepository, EfCompetitionRepository>();

            services
                .AddScoped<ITeamRepository, EfTeamRepository>();

            services
                .AddScoped<IPlayerRepository, EfPlayerRepository>();
        }

        private void SetupDb(IServiceCollection services)
        {
            var connString = Configuration.GetConnectionString("FootballDb");

            services
                .AddDbContext<FootballDbContext>(options => options.UseSqlServer(connString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<FootballDbContext>();
                
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Football Data Import API");
            });
        }
    }
}
