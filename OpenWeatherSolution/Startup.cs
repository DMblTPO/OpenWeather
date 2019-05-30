using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenWeatherSolution.Infrastructure;
using OpenWeatherSolution.Managers;
using OpenWeatherSolution.Middleware;
using OpenWeatherSolution.Models;

namespace OpenWeatherSolution
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IServiceProvider services)
        {
            Configuration = configuration;
            Services = services;
        }

        public IConfiguration Configuration { get; }
        public IServiceProvider Services { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var section = Configuration.GetSection(nameof(AppConfig));
            services.Configure<AppConfig>(section);
            services.AddSingleton(section.Get<AppConfig>());

            var con = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WeatherContext>(options => options.UseSqlServer(con));

            services.AddScoped<IWeatherManager, WeatherManager>();

            services
                .AddMvc(opts => { opts.ModelBinderProviders.Insert(0, new GetWeatherBinderProvider()); })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            app.UseExceptionHandler(err =>
            {
                err.Run(async ctx =>
                {
                    ctx.Response.StatusCode = 400;
                    ctx.Response.ContentType = "application/json";
                    var pathFeature = ctx.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = pathFeature?.Error;
                    var errorMsg = exception != null ? exception.Message : "Unexpected error";
                    await ctx.Response
                        .WriteAsync(JsonConvert.SerializeObject(new
                        {
                            error = errorMsg
                        }));
                });
            });
            //}

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseApiKeyChecker();

            app.UseMvc();
        }
    }
}
