using Quartz;
using Synchronization.Logs;
using Synchronization.Operations;
using Synchronization.Settings;

namespace Synchronization.Job
{
    public class SynchronizationJob : IJob
    {
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

            var _remove = new Removal();
            _remove.removeFiles(sourceFolder, destinationFolder);
            _log.info("Executing job.");
            return Task.CompletedTask;
        }
    }
}

