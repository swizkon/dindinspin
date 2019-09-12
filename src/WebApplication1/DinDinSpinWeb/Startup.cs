using AutoMapper;
using DinDinSpinWeb.GraphQL;
using DinDinSpinWeb.Hubs;
using DinDinSpinWeb.Infra.Db;
using Domain.Repositories;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DinDinSpinWeb
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
            services.AddSignalR();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //***< My services >*** 
            //services.AddHttpClient<ReservationHttpGraphqlClient>(x => x.BaseAddress = new Uri(Configuration["GraphQlEndpoint"]));
            //services.AddSingleton(t => new GraphQLClient(Configuration["GraphQlEndpoint"]));
            //services.AddSingleton<ReservationGraphqlClient>();
            //***</ My services >*** 

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            services.AddDbContext<DinDinSpinDbContext>(options => options.UseInMemoryDatabase(DinDinSpinDbContext.DbConnectionString));
            // services.AddDbContext<MyHotelDbContext>(options => options.UseSqlServer(MyHotelDbContext.DbConnectionString));
            services.AddTransient<DinnerRepository>();

            //***< GraphQL Services >*** 
            services.AddScoped<IDependencyResolver>(x => new FuncDependencyResolver(x.GetRequiredService));

            services.AddScoped<DinDinSpinSchema>();

            services.AddGraphQL(x =>
                {
                    x.ExposeExceptions = true; //set true only in dev mode.
                })
                .AddGraphTypes(ServiceLifetime.Scoped)
                .AddUserContextBuilder(httpContext => httpContext.User)
                .AddDataLoader();

            //***</ GraphQL Services >*** 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DinDinSpinDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            

            app.UseStaticFiles();
            app.UseSpaStaticFiles();


            app.UseGraphQL<DinDinSpinSchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); //to explorer API navigate https://*DOMAIN*/ui/playground
            
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalRCounter>("/signalr");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            InitializeMapper();
        }

        private static void InitializeMapper()
        {
            Mapper.Initialize(x =>
            {
                //x.CreateMap<Guest, GuestModel>();
                //x.CreateMap<Room, RoomModel>();
                //x.CreateMap<Reservation, ReservationModel>();
            });
        }
    }
}
