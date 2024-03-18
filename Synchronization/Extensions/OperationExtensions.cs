using Synchronization.Logs;
using Synchronization.Utils;

namespace Synchronization.Extensions
{
    public static class OperationExtensions
    {
        private static readonly CheckSumUtils _checkSum = new();
        private static readonly WriteLogFileUtils _logFile = new();
        private static List<FileInfo> fileList = new List<FileInfo>();

        /// <summary>
        /// Extention Method <c>Copy</c> copies file(s) from source folder to destination folder
        /// which will be tagged with a Checksum.
        /// </summary>
        public static void Copy(this DirectoryInfo source, DirectoryInfo target)
        {
            var _log = new ConsoleLog("Copy");

            GetAllDirectories(source)
                .ForEach(x =>
                {
                    var tempPath = x.Substring(source.FullName.Length);
                    var newCombinePath = Path.Combine(target.FullName, tempPath);
                    var value = new DirectoryInfo(newCombinePath);
                    if (!value.Exists)
                    {
                        Directory.CreateDirectory(newCombinePath);
                        _logFile.WriteFile("Directory Creation", "", x, newCombinePath);
                        _log.Info(string.Format("Directory(s) {0} created succesfully.", tempPath));
                    }
                });

            GetAllDirectories(source)
                .ForEach(x =>
                {
                    GetAllFiles(x)
                        .ForEach(sourceFile =>
                        { 
                            var tempPath = sourceFile.Substring(target.FullName.Length - 1);
                            var newPath = Path.Combine(target.FullName, tempPath);
                            if (!File.Exists(newPath))
                            {
                                if (File.Exists(sourceFile))
                                {
                                    _checkSum.SHA256CheckSum(sourceFile);
                                    new FileInfo(sourceFile).CopyTo(newPath, true);
                                    _logFile.WriteFile("copy", sourceFile, x, newPath);
                                    _log.Info(string.Format("File(s) {0} copied succesfully.", string.Join(",", sourceFile)));
                                }
                            }
                        });
                });

            source.Except(target)                 
                 .ForEach(file =>
                 {
                     if (!File.Exists(Path.Combine(target.FullName, Path.GetFileName(file.Name))))
                     {
                         _checkSum.SHA256CheckSum(Path.Combine(source.FullName, Path.GetFileName(file.Name)));
                         new FileInfo(Path.Combine(source.FullName, Path.GetFileName(file.Name)))
                            .CopyTo(Path.Combine(target.FullName, Path.GetFileName(file.Name)), true);
                         _logFile.WriteFile("copy", file.Name, source.FullName, target.FullName);
                         _log.Info(string.Format("File(s) {0} copied succesfully.", file.Name));
                     }
                 });
        }


        /// <summary>
        /// Extension Method <c>Update</c> updates file(s) from source folder to destination folder
        /// when the content has been modified in source folder. It compares the 
        /// SHA256 from each file to copy the ones whose algorithm might be different.
        /// </summary>
        public static void Update(this DirectoryInfo source, DirectoryInfo target)
        {
            var _log = new ConsoleLog("Update");

            GetAllDirectories(source)
               .ForEach(x =>
               {
                   GetAllFiles(x)
                   .ForEach(sourceFile =>
                   {
                       var tempPath = sourceFile.Substring(target.FullName.Length - 1);
                       var newPath = Path.Combine(target.FullName, tempPath);
                       if (File.Exists(newPath) && File.Exists(sourceFile))
                       {
                           var checkingSrc = _checkSum.SHA256CheckSum(sourceFile);
                           var checkingDest = _checkSum.SHA256CheckSum(newPath);
                           if (!String.Equals(checkingSrc, checkingDest))
                           {
                               _checkSum.SHA256CheckSum(sourceFile);
                               File.Copy(sourceFile, newPath, true);
                               _logFile.WriteFile("Update", sourceFile, x, newPath);
                               _log.Info(string.Format("File(s) {0} updated succesfully.", sourceFile));
                           }                               
                       }
                   });
               });

               fileList.AddFiles(source)
               .ForEach(s =>
               {
                   fileList.AddFiles(target)
                        .ForEach(d =>
                        {                            
                            if (string.Equals(s.Name, d.Name))
                            {
                                if (d.Exists)
                                {
                                    var checkingSrc = _checkSum.SHA256CheckSum(s.ToString());
                                    var checkingDest = _checkSum.SHA256CheckSum(d.ToString());
                                    if (!string.Equals(checkingSrc, checkingDest))
                                    {
                                        _checkSum.SHA256CheckSum(d.ToString());
                                        s.CopyTo(d.ToString(), true);
                                        _logFile.WriteFile("Update", s.Name, source.FullName, target.FullName);
                                        _log.Info(string.Format("File(s) {0} updated succesfully.", d.Name));
                                    }
                                }
                            }
                            
                        });
               });        
        }

            /// <summary>
            /// Extension Method <c>Delete</c> deletes file(s) from destination folder
            /// when the source folder does not have the same file(s) as in
            /// destination folder.
            /// </summary>
            public static void Delete(this DirectoryInfo source, DirectoryInfo target)
            {
                var _log = new ConsoleLog("Deleted");

                GetAllDirectories(target)
                    .ForEach(x =>
                    {
                        var tempPath = x.Substring(target.FullName.Length);
                        var newPath = Path.Combine(source.FullName, tempPath);
                        if (!Directory.Exists(newPath))
                        {
                            if (Directory.Exists(x))
                            {
                                Directory.Delete(x, true);
                                _logFile.WriteFile("Delete", x, "", "");
                                _log.Info(string.Format("Deleted: {0}", x));
                            }
                        }
                    });

                GetAllDirectories(target)
                    .ForEach(x =>
                    {
                        GetAllFiles(x)
                            .ForEach(targetFile =>
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
                            });
                    });

                target.Except(source)                    
                    .ForEach(file =>
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
                    });
            }

        private static List<string> GetAllDirectories(DirectoryInfo directoryInfo)
        {
            return Directory.GetDirectories(directoryInfo.FullName, "*.*", SearchOption.AllDirectories).ToList();
        }

        private static List<string> GetAllFiles(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
        }
    }
}

   