using Quartz;
using Synchronization.Logs;
using Synchronization.Operations;
using Synchronization.Settings;

namespace Synchronization.Job
{
    /// <summary>
    /// Class <c>SynchronizationJob</c> gathers the cycle of operation
    /// that has the job to be performed.
    /// </summary>
    public class SynchronizationJob : IJob
    {
        /// <summary>
        /// Method <c>Execute</c> gathers the cycle of operation
        /// that is triggered by the job.
        /// </summary>
        /// <returns>
        /// The successfully completed task.
        /// </returns>
        public Task Execute(IJobExecutionContext context)
        {
            var _log = new ConsoleLog("SynchronizationJob");
            var sourceFolder = FolderOptions.SourceFolder;
            var destinationFolder = FolderOptions.DestinationFolder;

            //It throws FilePathException in case of any mistake with the file paths
            if (string.IsNullOrWhiteSpace(sourceFolder) || string.IsNullOrWhiteSpace(destinationFolder))
                throw new FilePathException("No empty path is allowed!");

            var _update = new Update();
            _update.updateFiles(sourceFolder, destinationFolder);

            var _copy = new Copy();
            _copy.copyFiles(sourceFolder, destinationFolder);

            var _delete = new Delete();
            _delete.deleteFiles(sourceFolder, destinationFolder);

            _log.info("Executing job.");
            return Task.CompletedTask;
        }
    }
}

