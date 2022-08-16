using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Framework.Services
{
    public class TestConfigurationReader
    {
        private const string ConfigurationDirectoryPath =
            "C:/Users/User/source/repos/EpamQAAutomation/Framework/Configurations";

        public static ConfigurationModel configurationModel;

        public static ConfigurationModel TestConfiguration()
        {
            configurationModel = new ConfigurationModel();
            var builder = new ConfigurationBuilder();

            switch (TestContext.Parameters.Get("env"))
            {
                case "dev":
                    builder.AddJsonFile(ConfigurationDirectoryPath + "/dev_configsettings.json");
                    break;
                case "qa":
                    builder.AddJsonFile(ConfigurationDirectoryPath + "/qa_configsettings.json");
                    break;
                default:
                    builder.AddJsonFile(ConfigurationDirectoryPath + "/dev_configsettings.json");
                    break;
            }

            var configuration = builder.Build();
            configuration.Bind(configurationModel);

            return configurationModel;
        }
    }
}