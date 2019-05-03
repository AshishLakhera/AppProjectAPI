using NLog;
using System;

namespace Benchmarking.Logger
{
    public static class BMLogger
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        public static void Info(string Message)
        {
            _logger.Info(Message);
        }
        public static void Debug(string Message)
        {
            _logger.Debug( Message);
        }
        public static void Error(string Message)
        {
            _logger.Error(Message);
        }
    }
}
