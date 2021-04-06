using System.Collections.Generic;
using System.Linq;

namespace ScoreBoard
{
    internal class GamesRepositoryOnCollections : IGameRepository
    {
        private readonly ILogger _logger;
        private readonly List<IGame> _games = new List<IGame>();

        public GamesRepositoryOnCollections(ILogger logger)
        {
            _logger = logger;
        }

        public void AddGame(IGame game)
        {
            if (game == null || _games.Contains(game))
            {
                _logger.Log("Can't add a new game into repository because it is already exists game or arg is invalid");
            }
            else
            {
                game.Id = GetMaxId() + 1;
                _games.Add(game);
            }
        }

        public void RemoveGame(IGame game)
        {
            if (game != null && _games.Contains(game))
            {
                _games.Remove(game);
            }
            else
            {
                _logger.Log("Can't remove the game from repository because there is no such game or arg is invalid");
            }
        }

        public List<IGame> GetCurrentGames()
        {
            return _games;
        }

        public void UpdateGameScore(IGame gameWithNewScore)
        {
            if (gameWithNewScore != null && _games.Contains(gameWithNewScore))
            {
                var gameToUpdate = _games.First(g => g.Equals(gameWithNewScore));

                gameToUpdate.HomeTeamScore = gameWithNewScore.HomeTeamScore;
                gameToUpdate.AwayTeamScore = gameWithNewScore.AwayTeamScore;
            }
            else
            {
                _logger.Log("Can't update the game score in repository because there is no such game or arg is invalid");
            }
        }

        private int GetMaxId()
        {
            return _games.Count == 0 ? 0 : _games.Max(game => game.Id);
        }
    }
}
