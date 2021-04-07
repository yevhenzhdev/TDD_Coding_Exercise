namespace ScoreBoard
{
    /// <summary>
    /// Interface for class that provides logging feature
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Provides writing message by logging service
        /// </summary>
        /// <param name="message">Message to be witten</param>
        void Log(string message);
    }
}
