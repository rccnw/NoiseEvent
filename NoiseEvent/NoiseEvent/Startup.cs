using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoiseEvent.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using ApplicationCore.AppConfiguration;
using StringTokenFormatter;
using System.Diagnostics;
using System.Text;
using ApplicationCore.Utilities;
using Microsoft.Azure.KeyVault;
using Microsoft.VisualBasic.CompilerServices;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using ApplicationCore.Services;
using ApplicationCore.Logging;





namespace NoiseEvent
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env, IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;

            var pass = Configuration["SqlDbPassword"];

            _logger = logger;

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(env.ContentRootPath);

            // Azure makes all the values of the app settings available as environment variables.
            builder.AddEnvironmentVariables();

            // You do not need to explicitly add appsettings.json in ASP.NET Core 2
            // but to set the reloadOnChange property we do it anyway
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

     //       loggerFactory.AddConsole();
        //    loggerFactory.AddDebug()



            if (env.IsDevelopment())
            {
                // use 'secrets.json'  (See 'Manage User Secrets' right-click on Web project) 
                builder.AddUserSecrets<Startup>();

                builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            }

            //var keyvault = new KeyVaultHelper();

            if (env.IsProduction())
            {
            }

            //       app.UseStaticFiles();
            // app.UseStatusCodePages();

            Configuration = builder.Build();

            Debug.WriteLine("----------------------");
            Debug.WriteLine("Configuration values :");
            foreach (var item in configuration.AsEnumerable())
            {
                Configuration[item.Key] = item.Value;
                Debug.WriteLine(item.Value);
            }
            Debug.WriteLine("----------------------");

        }

        #region Configuration


        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureInMemoryDatabases(services);

            // use real database
            ConfigureProductionServices(services);
        }
        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            var dbName = Configuration["AppConfiguration:DatabaseName"];

            // use in-memory database
            services.AddDbContext<NoiseEventContext>(c => c.UseInMemoryDatabase(dbName));

            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284

            //services.AddDbContext<InformationDisplayContext>(c =>
            //    c.UseSqlServer(Configuration.GetConnectionString("SqlServerConnStr")));


#if DEBUG
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
#endif

            // set EF dbContext via DI
            services.AddDbContext<NoiseEventContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<LogSettings>(Configuration.GetSection("LogSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppConfiguration"));
            services.Configure<ApplicationInsightSettings>(Configuration.GetSection("ApplicationInsights"));
            


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IKeyVaultHelper, KeyVaultHelper>();

            // Repositories
            // These repositories provide access to Database via Entity Framework
            services.AddScoped<INoiseEventRepository, NoiseEventRepository>();

            // Application Service - business rules, access to repository
            services.AddScoped<INoiseEventService, NoiseEventService>();

            services.AddTransient<IApplicationInsightsLogger, ApplicationInsightsLogger>();
            // Add memory cache services
            services.AddMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services
            //    .AddMvcCore()
            //    .AddJsonFormatters()
            //    .AddApiExplorer()
            //    .AddAuthorization()
            //    .AddCors();

            services.AddAutoMapper(typeof(Startup));

           // services.Configure<RequestTelemetryEnricherOptions>(Configuration);
            services.AddApplicationInsightsTelemetry();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //});


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseSwagger();       // {yourBaseUrl}/swagger/v1/swagger.json
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            //});

        }
    }



}
