using System.Collections.Generic;
using System.Text;
using Ninject;
using ScoreBoard.Properties;

namespace ScoreBoard
{
    /// <summary>
    /// Class provides games service features
    /// </summary>
    public class GameService : IGameService
    {
        /// <summary>
        /// Object provides CRUD game operations in data storage
        /// </summary>
        private IGameRepository _gameRepository;

        /// <summary>
        /// Object provides logging features
        /// </summary>
        private ILogger _logger;

        #region Constructors
        /// <summary>
        /// Creates instance of <see cref="GameService"/>
        /// </summary>
        public GameService()
        {
            _gameRepository = new StandardKernel(new NinjectConfig()).Get<IGameRepository>();
            _logger = new StandardKernel(new NinjectConfig()).Get<ILogger>();
        }

        /// <summary>
        /// Creates instance of <see cref="GameService"/>
        /// </summary>
        /// <remarks>This constructor intended for unit testing purposes</remarks>
        internal GameService(IGameRepository gameRepository, ILogger logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Adds game into system
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        public void StartGame(string homeTeamName, string awayTeamName)
        {
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log(Resources.ServiceInvalidTeamNamesMsg);
            }
            else
            {
                _gameRepository.AddGame(new Game(homeTeamName, awayTeamName));
            }
        }

        /// <summary>
        /// Removes game from system
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        public void FinishGame(string homeTeamName, string awayTeamName)
        {
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log(Resources.ServiceInvalidTeamNamesMsg);
            }
            else
            {
                _gameRepository.RemoveGame(new Game(homeTeamName, awayTeamName));
            }
        }

        /// <summary>
        /// Updates csore in particular game
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="homeTeamNewScore">New value of home team score</param>
        /// <param name="awayTeamName">Away team name</param>
        /// <param name="awayTeamNewScore">New value of away team score</param>
        public void UpdateScore(string homeTeamName, int homeTeamNewScore, string awayTeamName, int awayTeamNewScore)
        {
            bool validationFailed = false;
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log(Resources.ServiceInvalidTeamNamesMsg);
                validationFailed = true;
            }
            if (homeTeamNewScore < 0 || awayTeamNewScore < 0)
            {
                _logger.Log(Resources.ServiceInvalidTeamScoresMsg);
                validationFailed = true;
            }
            if (validationFailed)
            {
                return;
            }

            var gameWithNewScore = new Game(homeTeamName, awayTeamName)
            {
                HomeTeamScore = homeTeamNewScore,
                AwayTeamScore = awayTeamNewScore
            };
            _gameRepository.UpdateGameScore(gameWithNewScore);
        }

        /// <summary>
        /// Gets a summary of games by total score
        /// </summary>
        /// <returns>String represents summary info of games by total score</returns>
        /// <remarks>Those games with the same total score will be returned ordered by 
        /// the most recently added to our system</remarks>
        public string GetGamesSummaryByTotalScore()
        {
            var currentGames = _gameRepository.GetCurrentGames();
            if (currentGames.Count == 0)
            {
                _logger.Log(Resources.ServiceNoGamesForSummaryMsg);
                return string.Empty;
            }
            else
            {
                return BuildGamesSummaryByTotalScore(currentGames);
            }
        }

        #region Private methods
        /// <summary>
        /// Builds games sammary information by total score with sorting
        /// </summary>
        /// <param name="games">Collection of games</param>
        /// <returns>String with games summary info</returns>
        private string BuildGamesSummaryByTotalScore(List<IGame> games)
        {
            games.Sort();
            var sb = new StringBuilder();
            foreach (var g in games)
            {
                sb.Append(g.HomeTeamName);
                sb.Append(" ");
                sb.Append(g.HomeTeamScore);
                sb.Append(" - ");
                sb.Append(g.AwayTeamName);
                sb.Append(" ");
                sb.Append(g.AwayTeamScore);
                sb.Append("\n\r");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Defines whether any team name is invalid.
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        /// <returns>True if names are valid, otherwise - false</returns>
        private bool AreTeamNamesValid(string homeTeamName, string awayTeamName)
        {
            if (string.IsNullOrEmpty(homeTeamName) ||
                string.IsNullOrEmpty(awayTeamName))
                return false;
            else
                return true;
        }
        #endregion
    }
}
