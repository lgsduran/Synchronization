using Synchronization.Logs;
using Synchronization.Settings;

namespace Synchronization.Utils
{
    /// <summary>
    /// Class <c>WriteLogeFileUtils</c> utility to save a log file.
    /// </summary>
    public class WriteLogFileUtils
    {
        /// <summary>
        /// Method <c>WriteFile</c> updates the log file according to the input action.
        /// <exception cref="Exception">
        /// Thrown when the file might be corrupted.
        /// </exception>
        /// </summary>
        public void WriteFile(string action, string fileNames, string srcFolder, string destFolder)
        {
            var _log = new ConsoleLog("WriteLogeFileUtils");
            string fileName = "SynchronizationLog.txt";
            try
            {
                using var sw = new StreamWriter(FolderOptions.LogFilePath + fileName, true);                
                    sw.WriteLine("Action: {0}", action);
                    sw.WriteLine("Source forder: {0}", srcFolder);
                    sw.WriteLine("Destination forder: {0}", destFolder);
                    sw.WriteLine("File name(s): {0}", fileNames);
                    sw.WriteLine("TimeStamp: {0}", DateTime.Now.ToString("MM/dd/yyyy HH:mm.ss"));
                    sw.WriteLine("");                
            }
            catch (Exception e)
            {
                _log.Error($"Log file failed: {e.Message}");
            }
        }
    }
}

