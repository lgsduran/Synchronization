using Synchronization.Extensions;

namespace Synchronization.Operations
{
    /// <summary>
    /// Class <c>Removal</c> deletes file(s) from destination folder.
    /// </summary>
    public class Delete
    {
        /// <summary>
        /// Method <c>removeFiles</c> deletes file(s) from destination folder
        /// when the source folder does not have the same file(s) as in
        /// destination folder.
        /// </summary>
        public void deleteFiles(string sourceFolder, string destinationFolder)
        {
            var sourceDir = new DirectoryInfo(sourceFolder);
            var destDir = new DirectoryInfo(destinationFolder);

            sourceDir.Delete(destDir);
        }
    }
}

