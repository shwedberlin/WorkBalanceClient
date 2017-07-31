namespace Infrastructure.Interfaces
{
    public interface ILogger
    {
        /// <summary>
        /// Logger Configuration.
        /// Logfile stored under \Log\UserName\logfile
        /// </summary>
        /// <param name="loggerName"></param>
        /// <param name="userName"></param>
        //void Configure(string loggerName, string userName);

        /// <summary>
        /// Logger Configuration.
        /// Logfile stored under \Log\logfile
        /// </summary>
        /// <param name="loggerName"></param>
        //void Configure(string loggerName);

        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Debug(string message);
    }
}
