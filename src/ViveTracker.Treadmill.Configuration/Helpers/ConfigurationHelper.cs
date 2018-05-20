using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ViveTracker.Treadmill.Configuration.Models;

namespace ViveTracker.Treadmill.Configuration.Helpers
{
    public static class ConfigurationHelper
    {
        private const string RelativePath = "ViveTracker.Treadmill";

        private const string ProfilesPath = "Profiles";

        private const string ConfigFile = "treadmill.config.json";

        public static string GetConfigurationPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), RelativePath);
            Directory.CreateDirectory(path);

            return path;
        }

        public static string GetProfilePath()
        {
            string path = Path.Combine(GetConfigurationPath(), ProfilesPath);
            Directory.CreateDirectory(path);

            return path;
        }

        public static string GetConfigFilePath()
        {
            string filePath = Path.Combine(GetConfigurationPath(), ConfigFile);

            if (!File.Exists(filePath))
            {
                var configFile = new ConfigurationFile();
                configFile.ResetDefault();

            }

            throw new NotImplementedException();
        }

        public static ConfigurationFile GetConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
