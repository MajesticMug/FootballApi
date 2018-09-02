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
using Sports.Football.Data.Model;
using Sports.Football.Repositories.Implementations;
using Sports.Football.Repositories.Interfaces;

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
            RegisterCoreComponents(services);
            RegisterRepositories(services);
            SetupDb(services);
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
                //context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
