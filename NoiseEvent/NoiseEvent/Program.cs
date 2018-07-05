using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;




namespace NoiseEvent
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {


            try
            {
                Log.Logger = new LoggerConfiguration()
                //.MinimumLevel.Debug()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

                Log.Information("NoiseEvent Program.Main starting...");

                //logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                //logger.Debug("init main");

                CreateWebHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }


        }

        // simple default version of CreateWebHostBuilder
        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)    
        //        .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            

                .ConfigureLogging((hostingContext, config) =>
                {
                    config.ClearProviders();
                })
                .ConfigureAppConfiguration((context, config) =>
                {       
                    var env = context.HostingEnvironment;

                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)    
                        //  .AddJsonFile("azurekeyvault.json", false, true)
                        .AddEnvironmentVariables();




                    var builtConfig = config.Build();

                    //var clientId = builtConfig["AppConfiguration:KeyVaultClientId"];
                    //var secret = builtConfig["AppConfiguration:KeyVaultSecret"];

                    //config.AddAzureKeyVault(
                    //    $"https://{builtConfig["AppConfiguration:Vault"]}.vault.azure.net/",
                    //    builtConfig["AppConfiguration:KeyVaultClientId"],
                    //    builtConfig["AppConfiguration:KeyVaultSecret"]);

                    config.AddAzureKeyVault(
                        $"https://NoiseEventKeyVault.vault.azure.net/",         //  
                        "c8a0a51b-d9e6-46b5-abba-cda619880e65",                 // 
                        "ErMO6WryjzDIJELT0akByklTWXB22HQBpacwr0IIkjI=");

                    // Gsts65shw@6%7
                    // https://noiseeventkeyvault.vault.azure.net/secrets/SqlDbPassword/96454d7cafba401da2129175597e90a0

                    //  var dbpass = 

                    var counr = config.Sources.Count;

                })




                //.ConfigureLogging((hostingContext, logging) =>
                //{
                //    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                //    logging.AddConsole();
                //    logging.AddDebug();
                //    logging.ClearProviders();
                //    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //})

                //.ConfigureAppConfiguration((ctx, builder) =>
                //    {
                //       // var token = new KeyVaultToken();


                //        var keyVaultEndpoint = GetKeyVaultEndpoint();
                //        if (!string.IsNullOrEmpty(keyVaultEndpoint))
                //        {
                //            var azureServiceTokenProvider = new AzureServiceTokenProvider();
                //            var keyVaultClient = new KeyVaultClient(
                //                new KeyVaultClient.AuthenticationCallback(
                //                    azureServiceTokenProvider.KeyVaultTokenCallback));
                //            builder.AddAzureKeyVault(
                //                keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                //        }
                //    }
                //)



                //.UseConfiguration(Configuration)

                .UseStartup<Startup>() // Specify the Startup class 
                .UseSerilog()       
            ;  
    }
}
