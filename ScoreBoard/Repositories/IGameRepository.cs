using System.Collections.Generic;

namespace ScoreProcessor
{
    /// <summary>
    /// Interface for class provides CRUD game operations in data storage
    /// </summary>
    internal interface IGameRepository
    {
        /// <summary>
        /// Adds game into data storage
        /// </summary>
        /// <param name="game">Game object to be added</param>
        void AddGame(IGame game);

        /// <summary>
        /// Removes game from data storage
        /// </summary>
        /// <param name="game">Game object to be removed</param>
        void RemoveGame(IGame game);

        /// <summary>
        /// Updates score in particular game
        /// </summary>
        /// <param name="game">Game object to be updated with new score</param>
        void UpdateGameScore(IGame game);

        /// <summary>
        /// Gets all current games currently present in system
        /// </summary>
        /// <returns>Collections of game objects</returns>
        List<IGame> GetCurrentGames();
    }
}
