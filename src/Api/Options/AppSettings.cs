using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Options
{
    public class AppSettings
    {
        public class ServiceBusAppSettings
        {
            public string Setting1;
            public int Setting2;
        }

        public class ApiSettings
        {
            public bool FormatJson { get; set; }
        }
        
        
        public ApiSettings Api { get; set; } = new ApiSettings();        

        // Static load helper methods. These could also be moved to a factory class.
        public static IConfigurationRoot GetConfiguration(string dir)
        {
            return GetConfiguration(dir, null);
        }

        public static IConfigurationRoot GetConfiguration(string dir, string environmentName)
        {
            if (string.IsNullOrEmpty(environmentName))
                environmentName = "Development";

            var builder = new ConfigurationBuilder()
                .SetBasePath(dir)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static AppSettings GetSettings(string dir)
        {
            return GetSettings(dir, null);
        }

        public static AppSettings GetSettings(string dir, string environmentName)
        {
            var config = GetConfiguration(dir, environmentName);
            return GetSettings(config);
        }

        public static AppSettings GetSettings(IConfiguration config)
        {
            return config.Get<AppSettings>();
        }
    }
}
