using Synchronization.Logs;
using Synchronization.Utils;

namespace Synchronization.Extensions
{
    public static class OperationExtensions
    {
        private static CheckSumUtils _checkSum = new CheckSumUtils();
        private static WriteLogFileUtils _logFile = new WriteLogFileUtils();

        /// <summary>
        /// Extention Method <c>Copy</c> copies file(s) from source folder to destination folder
        /// which will be tagged with a Checksum.
        /// </summary>
        public static void Copy(this DirectoryInfo source, DirectoryInfo target)
        {
            if (string.Equals(source.FullName, target.FullName, StringComparison.OrdinalIgnoreCase))
                throw new DirectoryNotFoundException("Directory not found!");

            var _checkSum = new CheckSumUtils();
            var _log = new ConsoleLog("Copy");
            var _logFile = new WriteLogFileUtils();

            foreach (var sourcePath in Directory.GetDirectories(source.FullName, "*.*", SearchOption.AllDirectories))
            {
                var tempPath = sourcePath.Substring(source.FullName.Length);
                var newCombinePath = Path.Combine(target.FullName, tempPath);
                var value = new DirectoryInfo(newCombinePath);
                if (value.Exists)
                    continue;

                Console.WriteLine(newCombinePath);
                Directory.CreateDirectory(newCombinePath);
                _logFile.WriteFile("Directory Creation", "", sourcePath, newCombinePath);
                _log.Info(string.Format("Directory(s) {0} created succesfully.", tempPath));
            }    

            foreach (var sourcePath in Directory.GetDirectories(source.FullName, "*.*", SearchOption.AllDirectories))
            {
                foreach (var sourceFile in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    var tempPath = sourceFile.Substring(target.FullName.Length-1);
                    var newPath = Path.Combine(target.FullName, tempPath);
                    if (!File.Exists(newPath))
                    {
                        if (File.Exists(sourceFile))
                        {
                            _checkSum.SHA256CheckSum(sourceFile);
                            new FileInfo(sourceFile).CopyTo(newPath, true);
                            _logFile.WriteFile("copy", sourceFile, sourcePath, newPath);
                            _log.Info(string.Format("File(s) {0} copied succesfully.", string.Join(",", sourceFile)));

                        }
                    }
                }
            }            

            var files = source.GetFiles().Except(target.GetFiles());
            if (files.Count() == 0) return;

            foreach (var file in files)
            {
                if (!File.Exists(Path.Combine(target.FullName, Path.GetFileName(file.Name))))
                {
                    _checkSum.SHA256CheckSum(Path.Combine(source.FullName, Path.GetFileName(file.Name)));
                    new FileInfo(Path.Combine(source.FullName, Path.GetFileName(file.Name)))
                        .CopyTo(Path.Combine(target.FullName, Path.GetFileName(file.Name)), true);
                    _logFile.WriteFile("copy", file.Name, source.FullName, target.FullName);
                    _log.Info(string.Format("File(s) {0} copied succesfully.", file.Name));
                }
            }
        }

        /// <summary>
        /// Extension Method <c>Update</c> updates file(s) from source folder to destination folder
        /// when the content has been modified in source folder. It compares the 
        /// SHA256 from each file to copy the ones whose algorithm might be different.
        /// </summary>
        public static void Update(this DirectoryInfo source, DirectoryInfo target)
        {
            if (string.Equals(source.FullName, target.FullName, StringComparison.OrdinalIgnoreCase))
                throw new DirectoryNotFoundException("Directory not found!");

            var _log = new ConsoleLog("Update");

            foreach (var sourcePath in Directory.GetDirectories(source.FullName, "*.*", SearchOption.AllDirectories))
            {
                foreach (var sourceFile in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    var tempPath = sourceFile.Substring(target.FullName.Length-1);
                    var newPath = Path.Combine(target.FullName, tempPath);
                    if (File.Exists(newPath) && File.Exists(sourceFile))
                    {
                        var checkingSrc = _checkSum.SHA256CheckSum(sourceFile);
                        var checkingDest = _checkSum.SHA256CheckSum(newPath);
                        if (String.Equals(checkingSrc, checkingDest))
                            continue;

                        _checkSum.SHA256CheckSum(sourceFile);
                        File.Copy(sourceFile, newPath, true);
                        _logFile.WriteFile("Update", sourceFile, sourcePath, newPath);
                        _log.Info(string.Format("File(s) {0} updated succesfully.", sourceFile));
                    }
                }
            }

            foreach (var src in source.GetFiles())
            {
                foreach (var dest in target.GetFiles())
                {
                    if (string.Equals(src.Name, dest.Name))
                    {
                        if (File.Exists(dest.ToString()))
                        {
                            var checkingSrc = _checkSum.SHA256CheckSum(src.ToString());
                            var checkingDest = _checkSum.SHA256CheckSum(dest.ToString());
                            if (String.Equals(checkingSrc, checkingDest))
                                continue;

                            _checkSum.SHA256CheckSum(dest.ToString());
                            src.CopyTo(dest.ToString(), true);
                            _logFile.WriteFile("Update", src.Name, source.FullName, target.FullName);
                            _log.Info(string.Format("File(s) {0} updated succesfully.", src.Name));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Extension Method <c>Delete</c> deletes file(s) from destination folder
        /// when the source folder does not have the same file(s) as in
        /// destination folder.
        /// </summary>
        public static void Delete(this DirectoryInfo source, DirectoryInfo target)
        {
            if (string.Equals(source.FullName, target.FullName, StringComparison.OrdinalIgnoreCase))
                throw new DirectoryNotFoundException("Directory not found!");

            var _log = new ConsoleLog("Deleted");

            foreach (var targetPath in Directory.GetDirectories(target.FullName, "*.*", SearchOption.AllDirectories))
            {
                var tempPath = targetPath.Substring(target.FullName.Length);
                var newPath = Path.Combine(source.FullName, tempPath);
                if (!Directory.Exists(newPath))
                {
                    if (Directory.Exists(targetPath))
                    {
                        Directory.Delete(targetPath, true);                       
                        _log.Info(string.Format("Deleted: {0}", newPath));
                    }                        
                }
            }

            foreach (var targetPath in Directory.GetDirectories(target.FullName, "*.*", SearchOption.AllDirectories))
            {
                foreach (var targetFile in Directory.GetFiles(targetPath, "*.*", SearchOption.AllDirectories))
                {
                    var tempPath = targetFile.Substring(target.FullName.Length);
                    var newPath = Path.Combine(source.FullName, tempPath);
                    if (!File.Exists(newPath))
                    {
                        if (File.Exists(targetFile))
                        {
                            File.Delete(targetFile);
                            _logFile.WriteFile("Delete", targetFile, "", newPath);
                            _log.Info(string.Format("File(s) {0} deleted succesfully.", targetFile));
                        }                            
                    }
                }
            }

            var files = target.GetFiles().Except(source.GetFiles());
            if (files.Count() == 0) return;

            foreach (var file in files)
            {
                if (!File.Exists(Path.Combine(source.FullName, Path.GetFileName(file.Name))))
                {
                    if (File.Exists(Path.Combine(target.FullName, Path.GetFileName(file.Name))))
                    {
                        File.Delete(Path.Combine(target.FullName, Path.GetFileName(file.Name)));
                        _logFile.WriteFile("Delete", file.Name, "", target.FullName);
                        _log.Info(string.Format("File(s) {0} deleted succesfully.", file.Name));
                    }                        
                }
            }
        }        
    }
}
   
