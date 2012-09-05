using System;
using Common.Logging;
using Common.Logging.Factory;
using log4net.Core;

namespace FileHelperHeaders.QuartzLog4net
{
    public class Log4NetLogger : AbstractLogger
    {
        static readonly Type declaringType = typeof (Log4NetLogger);
        readonly ILogger _logger;

        protected internal Log4NetLogger(ILoggerWrapper log)
        {
            _logger = log.Logger;
        }

        public override bool IsDebugEnabled
        {
            get { return _logger.IsEnabledFor(Level.Debug); }
        }

        public override bool IsErrorEnabled
        {
            get { return _logger.IsEnabledFor(Level.Error); }
        }

        public override bool IsFatalEnabled
        {
            get { return _logger.IsEnabledFor(Level.Fatal); }
        }

        public override bool IsInfoEnabled
        {
            get { return _logger.IsEnabledFor(Level.Info); }
        }

        protected override void WriteInternal(LogLevel logLevel, object message, Exception exception)
        {
            var level = GetLevel(logLevel);
            _logger.Log(declaringType, level, message, exception);
        }

        public override bool IsTraceEnabled
        {
            get { return _logger.IsEnabledFor(Level.Trace); }
        }

        public override bool IsWarnEnabled
        {
            get { return _logger.IsEnabledFor(Level.Warn); }
        }

        static Level GetLevel(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.All:
                    return Level.All;
                case LogLevel.Trace:
                    return Level.Trace;
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
                default:
                    throw new ArgumentOutOfRangeException("logLevel", logLevel, "unknown log level");
            }
        }
    }
}