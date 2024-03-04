using Synchronization.Logs;
using Synchronization.Utils;

namespace Synchronization.Operations
{
    /// <summary>
    /// Class <c>Update</c> updates file(s) whose the content has been modified in the source folder.
    /// </summary>
    public class Update
    {
        /// <summary>
        /// Method <c>updateFiles</c> updates file(s) from source folder to destination folder
        /// when the content has been modified in source folder. It compares the 
        /// SHA256 from each file to copy the ones whose algorithm might be different.
        /// </summary>
        public void updateFiles(string sourceFolder, string destinationFolder)
        {
            var _checkSum = new CheckSumUtils();
            var _log = new ConsoleLog("Update");
            var _filesDuplicatedTemp = new List<string>();
            var _logResults = new List<string>();

            var srcFiles = Directory.GetFiles(sourceFolder);
            var destFiles = Directory.GetFiles(destinationFolder);

            var srcFilesTemp = from s in srcFiles
                               where !string.IsNullOrEmpty(s)
                               select Path.GetFileName(s);

            var destFilesTemp = from d in destFiles
                                where !string.IsNullOrEmpty(d)
                                select Path.GetFileName(d);


            foreach (var srcFileTemp in srcFilesTemp)
            {
                foreach (var destFileTemp in destFilesTemp)
                {
                    if (string.Equals(srcFileTemp, destFileTemp))
                        _filesDuplicatedTemp.Add(srcFileTemp);
                }
            }

            foreach (var srcFileName in _filesDuplicatedTemp)
            {
                if (File.Exists(Path.Combine(destinationFolder, Path.GetFileName(srcFileName))))
                {
                    var checkingSrc = _checkSum.SHA256CheckSum(Path.Combine(sourceFolder, Path.GetFileName(srcFileName)));
                    var checkingDest = _checkSum.SHA256CheckSum(Path.Combine(destinationFolder, Path.GetFileName(srcFileName)));
                    if (String.Equals(checkingSrc, checkingDest))
                    {
                        continue;
                    }
                    else
                    {
                        _checkSum.SHA256CheckSum(Path.Combine(sourceFolder, Path.GetFileName(srcFileName)));
                        File.Copy(Path.Combine(sourceFolder, Path.GetFileName(srcFileName)), Path.Combine(destinationFolder, Path.GetFileName(srcFileName)), true);
                        _logResults.Add(srcFileName);
                    }
                }
            }

            if (_logResults.Count() > 0)
            {
                var _logFile = new WriteLogFileUtils();
                _logFile.WriteFile("update", string.Join(",", _logResults), sourceFolder, destinationFolder);
                _log.info(string.Format("File(s) {0} copied succesfully.", string.Join(",", _logResults)));
            }
        }
    }
}