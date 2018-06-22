using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NoiseEvent
{
    public class Program
    {
        public static void Main(string[] args)
        {


            try
            {
                //logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                //logger.Debug("init main");

                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception /*ex*/)
            {
                // NLog: catch setup errors
               // logger?.Error(ex, $"Stopped program because of exception. {ex.Message}");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                //NLog.LogManager.Shutdown();
            }


        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                .ConfigureAppConfiguration((context, config) =>
                {

                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false)
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





                .UseStartup<Startup>() // Specify the Startup class 
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
              //  .UseNLog()
            ;  
    }
}
