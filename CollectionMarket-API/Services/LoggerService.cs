﻿using CollectionMarket_API.Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class LoggerService : ILoggerService
    {
        private static ILogger _logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogException(Exception e)
        {
            LogError($"{e.Message} - {e.InnerException}");
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
