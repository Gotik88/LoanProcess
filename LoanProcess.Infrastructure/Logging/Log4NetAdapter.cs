// ============================================================================
// <copyright file="Log4NetAdapter.cs" company="Dmytro Romanii">
//   Copyright (c) Dmytro Romanii 2014. All rights reserved.
// </copyright>
// ============================================================================

namespace LoanProcess.Infrastructure.Logging
{
    public class Log4NetAdapter : ILogger
    {
        ////private readonly log4net.ILog _log;

        public Log4NetAdapter()
        {
            /* XmlConfigurator.Configure();
             _log = LogManager
             .GetLogger(ApplicationSettingsFactory.GetApplicationSettings()
             .LoggerName);*/
        }

        public void Log(string message)
        {
            //// _log.Info(message);
        }
    }
}
