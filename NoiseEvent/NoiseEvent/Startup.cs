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
using ApplicationCore.Configuration;
using StringTokenFormatter;
using System.Diagnostics;
using System.Text;
using ApplicationCore.Utilities;
using Microsoft.Azure.KeyVault;
using Microsoft.VisualBasic.CompilerServices;
using System.Diagnostics;



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

 

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(typeof(Startup));
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
        }
    }
}
