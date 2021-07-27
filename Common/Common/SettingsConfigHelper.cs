using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common
{
    public class SettingsConfigHelper
    {
        private static SettingsConfigHelper _appSettings;

        public string AppSettingValue { get; set; }

        public static string AppSetting(string section, string key)
        {
            _appSettings = GetCurrentSettings(section, key);
            return _appSettings.AppSettingValue;
        }

        public SettingsConfigHelper(IConfiguration config, string key)
        {
            AppSettingValue = config.GetValue<string>(key);
        }

        public static SettingsConfigHelper GetCurrentSettings(string section, string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var settings = new SettingsConfigHelper(configuration.GetSection(section), key);

            return settings;
        }
    }
}
