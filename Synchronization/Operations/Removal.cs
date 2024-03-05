using Synchronization.Logs;
using Synchronization.Utils;

namespace Synchronization.Operations
{
    /// <summary>
    /// Class <c>Removal</c> deletes file(s) from destination folder.
    /// </summary>
    public class Removal
    {
        /// <summary>
        /// Method <c>removeFiles</c> deletes file(s) from destination folder
        /// when the source folder does not have the same file(s) as in
        /// destination folder.
        /// </summary>
        public void removeFiles(string sourceFolder, string destinationFolder)
        {
            var _log = new ConsoleLog("Removal");
            var srcFiles = Directory.GetFiles(sourceFolder);
            var destFiles = Directory.GetFiles(destinationFolder);

            var srcFilesTemp = from s in srcFiles
                               where !string.IsNullOrEmpty(s)
                               select Path.GetFileName(s);

            var destFilesTemp = from d in destFiles
                                where !string.IsNullOrEmpty(d)
                                select Path.GetFileName(d);

            var UnkownFileInDestFolder = destFilesTemp.Except(srcFilesTemp);

            if (UnkownFileInDestFolder.Count() == 0) return;

            foreach (var descFileName in UnkownFileInDestFolder)
            {
                if (File.Exists(Path.Combine(destinationFolder, Path.GetFileName(descFileName))))
                    File.Delete(Path.Combine(destinationFolder, Path.GetFileName(descFileName)));                
            }
            var _logFile = new WriteLogFileUtils();
            _logFile.WriteFile("removal", string.Join(",", UnkownFileInDestFolder), sourceFolder, destinationFolder);
            _log.info(string.Format("File(s) {0} removed succesfully.", string.Join(",", UnkownFileInDestFolder)));
        }
    }
}

