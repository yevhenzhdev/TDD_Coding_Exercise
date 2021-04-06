using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard
{
    class GamesRepositoryOnCollections : IGameRepository
    {
        private readonly ILogger _logger;
        private readonly List<IGame> _games = new List<IGame>();

        public GamesRepositoryOnCollections(ILogger logger)
        {
            _logger = logger;
        }

        public void AddGame(IGame game)
        {
            throw new NotImplementedException();
        }

        public List<IGame> GetCurrentGames()
        {
            throw new NotImplementedException();
        }

        public void RemoveGame(IGame game)
        {
            throw new NotImplementedException();
        }

        public void UpdateGameScore(IGame game)
        {
            throw new NotImplementedException();
        }
    }
}
