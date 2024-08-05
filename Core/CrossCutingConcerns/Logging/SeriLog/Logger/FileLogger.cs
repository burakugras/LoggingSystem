using Core.CrossCutingConcerns.Logging.SeriLog.ConfigurationModels;
using Core.CrossCutingConcerns.Logging.SeriLog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging.SeriLog.Logger
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger(IConfiguration configuration)
        {
            FileLogConfiguration logConfig =
                configuration.GetSection("SeriLogConfigurations:FileLogConfiguration").Get<FileLogConfiguration>()
                ?? throw new Exception(SerilogMessages.NullOptionsMessage);

            string logFilePath = string.Format(format: "{0}{1}", arg0: Directory.GetCurrentDirectory() + logConfig.FolderPath, arg1: ".txt");

            Logger = new LoggerConfiguration().WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 5000000,
                shared: true,
                encoding: Encoding.UTF8,
                outputTemplate: "[{Date:HH:mm:ss}] {Description}{NewLine}{Exception}"
                ).CreateLogger();
        }
    }
}
