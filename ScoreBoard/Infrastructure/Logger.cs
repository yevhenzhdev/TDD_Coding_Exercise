using System.Diagnostics;

namespace ScoreBoard.Infrastructure
{
    class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
