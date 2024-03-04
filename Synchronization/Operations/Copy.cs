using Synchronization.Logs;
using Synchronization.Utils;

namespace Synchronization.Operations
{
    /// <summary>
    /// Class <c>Copy</c> copies file(s) from source folder to destination folder.
    /// </summary>
    public class Copy
    {
        /// <summary>
        /// Method <c>copyFiles</c> copies file(s) from source folder to destination folder
        /// which will be tagged with a Checksum.
        /// </summary>
        public void copyFiles(string sourceFolder, string destinationFolder)
        {

            var _log = new ConsoleLog("Copy");
            var _checkSum = new CheckSumUtils();
            var srcFiles = Directory.GetFiles(sourceFolder);
            var destFiles = Directory.GetFiles(destinationFolder);

            var srcFilesResult = from s in srcFiles
                                 where !string.IsNullOrEmpty(s)
                                 select Path.GetFileName(s);

            var destFilesResult = from d in destFiles
                                  where !string.IsNullOrEmpty(d)
                                  select Path.GetFileName(d);

            var tempFiles = srcFilesResult.Except(destFilesResult);

            if (tempFiles.Count() == 0) return;

            foreach (var srcFileName in tempFiles)
            {
                if (!File.Exists(Path.Combine(destinationFolder, Path.GetFileName(srcFileName))))
                {
                    _checkSum.SHA256CheckSum(Path.Combine(sourceFolder, Path.GetFileName(srcFileName)));
                    File.Copy(Path.Combine(sourceFolder, Path.GetFileName(srcFileName)), Path.Combine(destinationFolder, Path.GetFileName(srcFileName)));

                }
            }
            var _logFile = new WriteLogFileUtils();
            _logFile.WriteFile("copy", string.Join(",", tempFiles), sourceFolder, destinationFolder);
            _log.info(string.Format("File(s) {0} copied succesfully.", string.Join(",", tempFiles)));
        }
    }
}
