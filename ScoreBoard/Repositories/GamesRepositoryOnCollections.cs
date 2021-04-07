using System.Collections.Generic;
using System.Linq;
using ScoreBoard.Properties;

namespace ScoreBoard
{
    internal class GamesRepositoryOnCollections : IGameRepository
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Collection for storage games
        /// </summary>
        private readonly List<IGame> _games = new List<IGame>();

        public GamesRepositoryOnCollections(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds game into data storage
        /// </summary>
        /// <param name="game">Game object to be added</param>
        public void AddGame(IGame game)
        {
            if (game != null && !_games.Contains(game))
            {
                game.Id = GetMaxId() + 1;
                _games.Add(game);
            }
            else
            {
                _logger.Log(Resources.RepositoryCantAddMsg);
            }
        }

        /// <summary>
        /// Removes game from data storage
        /// </summary>
        /// <param name="game">Game object to be removed</param>
        public void RemoveGame(IGame game)
        {
            if (game != null && _games.Contains(game))
            {
                _games.Remove(game);
            }
            else
            {
                _logger.Log(Resources.RepositoryCantRemoveMsg);
            }
        }

        /// <summary>
        /// Updates score in particular game
        /// </summary>
        /// <param name="gameWithNewScore">Game object with new score values</param>
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
                _logger.Log(Resources.RepositoryCantUpdateMsg);
            }
        }

        /// <summary>
        /// Gets all games currently present in system
        /// </summary>
        /// <returns>Collections of game objects</returns>
        public List<IGame> GetCurrentGames()
        {
            return _games;
        }

        /// <summary>
        /// Gets maximal game id value in collection. If collection is empty - returns zero
        /// </summary>
        /// <returns>Value of maximal id</returns>
        private int GetMaxId()
        {
            return _games.Count == 0 ? 0 : _games.Max(game => game.Id);
        }
    }
}
