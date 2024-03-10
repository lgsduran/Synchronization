﻿using Microsoft.Extensions.Logging;

namespace Synchronization.Logs
{
    /// <summary>
    /// Class <c>ConsoleLog</c> has the methods to print the logs on the console.
    /// </summary>
    public class ConsoleLog
    {
        private string className;

        public ConsoleLog(string className)
        {
            this.className = className;
        }

        /// <summary>
        /// Method <c>info</c> formats and writes an informational log message.
        /// </summary>
        public void Info(string message)
        {
            ConsoleLogger().LogInformation(message);
        }

        /// <summary>
        /// Method <c>error</c> formats and writes an error log message.
        /// </summary>
        public void Error(string message)
        {
            ConsoleLogger().LogError(message);
        }

        /// <summary>
        /// Method <c>ConsoleLogger</c> is responsible to instantiate the ILogger
        /// </summary>
        /// <returns>
        /// a type to perform logging.
        /// </returns>
        private ILogger ConsoleLogger()
        {
            using ILoggerFactory loggerFactory =
            LoggerFactory.Create(builder =>
            builder.AddSimpleConsole(options =>
            {
                options.IncludeScopes = true;
                options.SingleLine = true;
                options.TimestampFormat = "MM/dd/yyyy HH:mm:ss ";
            }));
            ILogger logger = loggerFactory.CreateLogger(this.className);
            return logger;
        }
    }
}

