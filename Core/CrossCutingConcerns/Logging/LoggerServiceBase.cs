﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging
{
    public abstract class LoggerServiceBase
    {
        public  ILogger Logger { get; set; }

        public void Fatal(string message) => Logger.Fatal(message);
        public void Info(string message) => Logger.Information(message);
        public void Warn(string message) => Logger.Warning(message);
        public void Debug(string message) => Logger.Debug(message);
        public void Error(string message) => Logger.Error(message);
        public void Verbose(string message) => Logger.Verbose(message);

    }
}
