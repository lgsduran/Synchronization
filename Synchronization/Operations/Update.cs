using Synchronization.Extensions;

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
            var sourceDir = new DirectoryInfo(sourceFolder);
            var destDir = new DirectoryInfo(destinationFolder);

            sourceDir.Update(destDir);                    
        }
    }
}