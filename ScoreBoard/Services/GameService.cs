using Ninject;
using System.Collections.Generic;
using System.Text;

namespace ScoreBoard
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private ILogger _logger;

        #region Constructors
        public GameService()
        {
            _gameRepository = new StandardKernel(new NinjectConfig()).Get<IGameRepository>();
            _logger = new StandardKernel(new NinjectConfig()).Get<ILogger>();
        }

        internal GameService(IGameRepository gameRepository, ILogger logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }
        #endregion

        public void StartGame(string homeTeamName, string awayTeamName)
        {
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log("Team names are invalid");
            }
            else
            {
                _gameRepository.AddGame(new Game(homeTeamName, awayTeamName));
            }
        }

        public void FinishGame(string homeTeamName, string awayTeamName)
        {
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log("Team names are invalid");
            }
            else
            {
                _gameRepository.RemoveGame(new Game(homeTeamName, awayTeamName));
            }
        }

        public void UpdateScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
        {
            bool validationFailed = false;
            if (!AreTeamNamesValid(homeTeamName, awayTeamName))
            {
                _logger.Log("Team names are invalid");
                validationFailed = true;
            }
            if (homeTeamScore < 0 || awayTeamScore < 0)
            {
                _logger.Log("Team scores are invalid");
                validationFailed = true;
            }
            if (validationFailed)
            {
                return;
            }

            var gameWithNewScore = new Game(homeTeamName, awayTeamName)
            {
                HomeTeamScore = homeTeamScore,
                AwayTeamScore = awayTeamScore
            };
            _gameRepository.UpdateGameScore(gameWithNewScore);
        }

        public string GetGamesSummaryByTotalScore()
        {
            var currentGames = _gameRepository.GetCurrentGames();
            if (currentGames.Count == 0)
            {
                _logger.Log("Can't get games summary by total score due to games absence in repository");
                return string.Empty;
            }
            else
            {
                return BuildGamesSummaryByTotalScore(currentGames);
            }
        }

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

        private bool AreTeamNamesValid(string homeTeamName, string awayTeamName)
        {
            if (string.IsNullOrEmpty(homeTeamName) ||
                string.IsNullOrEmpty(awayTeamName))
                return false;
            else
                return true;
        }
    }
}
