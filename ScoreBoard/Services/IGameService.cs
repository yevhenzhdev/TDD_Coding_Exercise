namespace ScoreBoard
{
    /// <summary>
    /// Interface for class that provides games service features
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Adds game into system
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        void StartGame(string homeTeamName, string awayTeamName);

        /// <summary>
        /// Removes game from system
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        void FinishGame(string homeTeamName, string awayTeamName);

        /// <summary>
        /// Updates csore in particular game
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="homeTeamNewScore">New value of home team score</param>
        /// <param name="awayTeamName">Away team name</param>
        /// <param name="awayTeamNewScore">New value of away team score</param>
        void UpdateScore(string homeTeamName, int homeTeamNewScore, string awayTeamName, int awayTeamNewScore);

        /// <summary>
        /// Gets a summary of games by total score
        /// </summary>
        /// <returns>String represents summary info of games by total score</returns>
        /// <remarks>Those games with the same total score will be returned ordered by 
        /// the most recently added to our system</remarks>
        string GetGamesSummaryByTotalScore();
    }
}
