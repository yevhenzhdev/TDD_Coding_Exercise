using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard
{
    class GameService : IGameService
    {
        private IGameRepository _gameRepository;
        private ILogger _logger;

        #region Constructors
        internal GameService(IGameRepository gameRepository, ILogger logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }
        #endregion
        public void FinishGame(string homeTeamName, string awayTeamName)
        {
            throw new NotImplementedException();
        }

        public string GetGamesSummaryByTotalScore()
        {
            throw new NotImplementedException();
        }

        public void StartGame(string homeTeamName, string awayTeamName)
        {
            throw new NotImplementedException();
        }

        public void UpdateScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
        {
            throw new NotImplementedException();
        }
    }
}
