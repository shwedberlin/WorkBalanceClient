namespace Logger
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Principal;

    using log4net;
    using log4net.Appender;
    using log4net.Core;
    using log4net.Layout;
    using log4net.Repository.Hierarchy;
    using Interfaces = Infrastructure.Interfaces;

    internal class Log4NetLogger : Interfaces.ILogger
    {
        private ILog logger;
        //private bool isConfigured = false;

        public Log4NetLogger()
        {        
            Configure("WBC");
        }

        private void Configure(string loggerName = "default")
        {
            GlobalContext.Properties["pid"] = Process.GetCurrentProcess().Id;

            logger = LogManager.GetLogger(loggerName);

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.RemoveAllAppenders();

            PatternLayout patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{ABSOLUTE} [%5property{pid}] %5level - %message%newline"
            };
            patternLayout.ActivateOptions();

            RollingFileAppender roller = new RollingFileAppender();
            roller.File = @"Log\logfile";

            roller.StaticLogFileName = false;
            roller.AppendToFile = true;
            roller.RollingStyle = RollingFileAppender.RollingMode.Composite;
            roller.DatePattern = "_yyyy.MM.dd\".log\"";
            roller.MaxSizeRollBackups = 24;
            roller.MaximumFileSize = "2MB";
            roller.Layout = patternLayout;
            roller.ActivateOptions();
            hierarchy.Root.AddAppender(roller);

            hierarchy.Root.Level = Level.Debug;
            hierarchy.Configured = true;

            //isConfigured = true;
        }

        public void Info(string message)
        {
            //if (!isConfigured)
            //    throw new LvsException("Logger should be configured first. Use 'Configure' method.");
            logger.Info(message);
        }

        public void Warning(string message)
        {
            //if (!isConfigured)
            //    throw new LvsException("Logger should be configured first. Use 'Configure' method.");
            logger.Warn(message);
        }

        public void Error(string message)
        {
            //if (!isConfigured)
            //    throw new LvsException("Logger should be configured first. Use 'Configure' method.");
            logger.Error(message);
        }

        public void Debug(string message)
        {
            //if (!isConfigured)
            //    throw new LvsException("Logger should be configured first. Use 'Configure' method.");
            logger.Debug(message);
        }
    }
}
