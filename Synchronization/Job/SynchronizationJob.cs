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
            var source = FolderOptions.SourceFolder;
            var target = FolderOptions.DestinationFolder;

            //It throws FilePathException in case of any mistake with the file paths
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(target))
                throw new FilePathException("No empty path is allowed!");

            new OperationImpl()
                .InitializeOperation(source, target)
                .Update()
                .Copy()
                .Delete();

            _log.Info("Executing job.");
            return Task.CompletedTask;
        }
    }
}

