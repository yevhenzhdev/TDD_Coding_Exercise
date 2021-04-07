using System;

namespace ScoreBoard
{
    /// <summary>
    /// Interface for classes represent sport game
    /// </summary>
    internal interface IGame : IEquatable<IGame>, IComparable<IGame>
    {
        /// <summary>
        /// Home team name
        /// </summary>
        string HomeTeamName { get; }

        /// <summary>
        /// Away team name
        /// </summary>
        string AwayTeamName { get; }

        /// <summary>
        /// Home team score
        /// </summary>
        int HomeTeamScore { get; set; }

        /// <summary>
        /// Away team score
        /// </summary>
        int AwayTeamScore { get; set; }

        /// <summary>
        /// Game id
        /// </summary>
        int Id { get; set; }
    }
}
