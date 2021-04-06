using System.Diagnostics;

namespace ScoreBoard
{
    class LoggerToDebug : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
