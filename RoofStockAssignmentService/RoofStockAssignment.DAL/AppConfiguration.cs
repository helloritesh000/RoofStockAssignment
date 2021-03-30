using Microsoft.Extensions.Configuration;

using System.IO;
using Microsoft.Extensions.Configuration.Json;

namespace RoofStockAssignment.DAL
{
    public class AppConfiguration
    {
        private static IConfiguration Configuration;
        static AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static string Get(string name)
        {
            string appSettings = Configuration[name];
            return appSettings;
        }

        public static IConfiguration GetConfiguration()
        {
            return Configuration;
        }
    }
}

