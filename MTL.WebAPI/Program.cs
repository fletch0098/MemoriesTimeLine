using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using MTL.DataAccess;

namespace MTL.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                //Get Logging service
                var logger = services.GetRequiredService<ILogger<Program>>();

                logger.LogInformation("Application Start");

                //Seed DB
                try
                {
                    var context = services.GetRequiredService<MyAppContext>();

                    var dbInitializer = services.GetRequiredService<DbInitializer>();

                    dbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }

                host.Run();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)

                .UseStartup<Startup>()

                //Appsetting
                .ConfigureAppConfiguration((webHostBuilderContext, configurationbuilder) =>
                {
                    var environment = webHostBuilderContext.HostingEnvironment;
                    string commonSettingFileName = "CommonSettings.json";
                    if (environment.EnvironmentName == "Development")
                    {
                        commonSettingFileName = "CommonSettings.Development.json";
                    }

                    string pathOfCommonSettingsFile = Path.Combine(environment.ContentRootPath, "..", @"MTL.Library\Common");
                    string pathOfProdCommonSettingsFile = Path.Combine(environment.ContentRootPath, @"Common");
                    configurationbuilder
                            .AddJsonFile("appSettings.json", optional: true)
                            .AddJsonFile(Path.Combine(pathOfCommonSettingsFile, commonSettingFileName), optional: true)
                            .AddJsonFile(Path.Combine(pathOfProdCommonSettingsFile, commonSettingFileName), optional: true)
                            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true);

                    configurationbuilder.AddEnvironmentVariables();
                })

                //Microsoft Logging Providor
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddAzureWebAppDiagnostics();
                    logging.AddDebug();
                })

                // NLog: setup NLog for Dependency injection
                .UseNLog()

                .Build();
    }
}
