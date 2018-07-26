using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MTL.AngularUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
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
                });
    }
}
