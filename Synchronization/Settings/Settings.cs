using Microsoft.Extensions.Configuration;
using Synchronization.Logs;

namespace Synchronization.Settings
{
    public static class Settings
    {
        private static ConsoleLog _log = new ConsoleLog("Settings");
        /// <summary>
        /// Method <c>GetSettings</c> loads application configuration properties.
        /// </summary>
        public static void GetSettings()
        {
            var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    //.AddJsonFile(settingsPath, optional: false);
                    .AddJsonFile("appsettings.json").Build();
            try
            {
                //IConfiguration config = builder.Build();
                FolderOptions.SourceFolder = config["Folders:SourceFolder"];
                FolderOptions.DestinationFolder = config["Folders:DestinationFolder"];
                FolderOptions.LogFilePath = config["Folders:LogFilePath"];
                _log.Info("appsettings file is loaded properly.");
            }
            catch (Exception e)
            {
                _log.Error($"Settings failed: {e.Message}");
            }
        }
    }
}

