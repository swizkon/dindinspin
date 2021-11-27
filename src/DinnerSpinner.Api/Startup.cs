using System.Collections.Generic;
using DinnerSpinner.Api.Domain.Contracts;
using DinnerSpinner.Api.Domain.Models;
using DinnerSpinner.Api.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DinnerSpinner.Api
{
    using Configuration;

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
            // requires using Microsoft.Extensions.Options
            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<SpinnerService>();

            services.AddSwaggerDocumentation();

            services.AddControllers();

            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var service = services.BuildServiceProvider().GetService<SpinnerService>();
            var spinner = service.Create(new CreateSpinner
            {
                Name = "Fam Jerndin",
                OwnerName = "Jonas Jerndin",
                OwnerEmail = "jonas@example.com"
            }).Result;

            var dd = service.AddDinner(spinner.Id, new Dinner
            {
                Name = "Saucisse au four",
                Link = "https://www.cuisineactuelle.fr/recettes/saucisse-au-four-319676",
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient
                    {
                        Name = "Kryddig korv"
                    }
                }
                // 
            }).Result;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.ToUpper() == "DEVELOPMENT")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();

            app.UseSwaggerDocumentation();

            app.UseCors("AllowAll");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(config => {
                config.MapControllers();
            });
        }
    }
}