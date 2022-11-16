using Microsoft.Extensions.Configuration;
using System.IO;

namespace CoreNRF.Utils
{
    static class StaticConfigurationManager
    {
        public static IConfiguration AppSetting
        {
            get;

        }

        static StaticConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        }

    }
}
