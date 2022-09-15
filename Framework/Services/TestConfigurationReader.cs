using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Framework.Services
{
    public class TestConfigurationReader
    {
        private const string ConfigurationDirectoryPath =
            "../../../Configurations";

        public static ConfigurationModel configurationModel;

        public static ConfigurationModel TestConfiguration()
        {
            configurationModel = new ConfigurationModel();
            var builder = new ConfigurationBuilder();

            var path = Path.Combine
            (
                Environment.CurrentDirectory, 
                ConfigurationDirectoryPath, 
                TestContext.Parameters.Get("env") switch
                {
                    "dev" => "dev_configsettings.json",
                    "qa" => "qa_configsettings.json",
                    _ => "dev_configsettings.json",
                }
            );
            
            builder.AddJsonFile(path);

            var configuration = builder.Build();
            configuration.Bind(configurationModel);

            return configurationModel;
        }
    }
}