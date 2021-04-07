using System.Diagnostics;

namespace ScoreBoard
{
    /// <summary>
    /// Class provides logging into Debug output
    /// </summary>
    class LoggerToDebug : ILogger
    {
        /// <summary>
        /// Writes log message into Debug output
        /// </summary>
        /// <param name="message">Message to be written into Debug output</param>
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
