using Synchronization.Extensions;

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
            var sourceDir = new DirectoryInfo(sourceFolder);
            var destDir = new DirectoryInfo(destinationFolder);           
                
            sourceDir.Copy(destDir);            
        }
    }
}
