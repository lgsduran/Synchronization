using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using Synchronization.Job;
using Synchronization.Logs;
using Synchronization.Settings;

class Program
{
    private static ConsoleLog _log = new ConsoleLog("Main");
    static async Task Main(string[] args)
    {
        //string _settingsPath = @"/Users/luizduran/Projects/Synchronization/Synchronization/Settings/appsettings.json";
        var _settingsPath = "/app/settings/appsettings.json";
        //Load paths
        GetSettings(_settingsPath);
        var _schedulerFactory = new StdSchedulerFactory();
        var scheduler = await _schedulerFactory.GetScheduler();

        // and start it off
        await scheduler.Start();

        // define the job and tie it to the class
        var job = JobBuilder.Create<SynchronizationJob>()
            .WithIdentity("SynchronizationJob", "SynchronizationGroup")
            .Build();

        // Trigger the job to run now, and then repeat according to the seconds
        var trigger = TriggerBuilder.Create()
            .WithIdentity("SynchronizationTrigger", "SynchronizationGroup")
            .StartNow()
            .WithSimpleSchedule(x => x
                .WithIntervalInSeconds(15)
                .RepeatForever())
            .Build();

        // Tell Quartz to schedule the job using our trigger
        await scheduler.ScheduleJob(job, trigger);

        // some sleep to show what's happening
        await Task.Delay(TimeSpan.FromSeconds(120));

        // and last shut down the scheduler when you are ready to close your program
        await scheduler.Shutdown();
        _log.Info("scheduler has been shut down.");

        //Console.WriteLine("Press any key to close the application");
        //Console.ReadKey();
    }

    /// <summary>
    /// Method <c>GetSettings</c> loads application configuration properties.
    /// </summary>
    private static void GetSettings(string settingsPath)
    {
        var builder = new ConfigurationBuilder()
                .AddJsonFile(settingsPath, optional: false);

        try
        {
            IConfiguration config = builder.Build();
            FolderOptions.SourceFolder = config["Folders:SourceFolder"];
            FolderOptions.DestinationFolder = config["Folders:DestinationFolder"];
            FolderOptions.LogFilePath = config["Folders:LogFilePath"];
            _log.Info("appsettings file is loaded properly.");
        }
        catch (Exception e)
        {
            _log.Error($"Settings failed: {e.Message}");
        }
    }
}