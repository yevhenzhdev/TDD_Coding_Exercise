namespace ScoreBoard
{
    /// <summary>
    /// Class represents sport game
    /// </summary>
    internal class Game : IGame
    {
        public Game(string homeTeamName, string awayTeamName)
        {
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
        }

        #region Properties
        /// <summary>
        /// Home team name
        /// </summary>
        public string HomeTeamName { get; private set; }

        /// <summary>
        /// Away team name
        /// </summary>
        public string AwayTeamName { get; private set; }

        /// <summary>
        /// Home team score
        /// </summary>
        public int HomeTeamScore { get; set; }

        /// <summary>
        /// Away team score
        /// </summary>
        public int AwayTeamScore { get; set; }

        /// <summary>
        /// Game id
        /// </summary>
        public int Id { get; set; }
        #endregion

        #region Interfaces methods implementations
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type
        /// </summary>
        /// <param name="obj">An object to compare with this object</param>
        /// <returns>true if the current object is equal to the other parameter, otherwise - false</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as IGame);
        }

        /// <summary>
        /// Indicates whether the current game is equal to another game
        /// </summary>
        /// <param name="other">A game to compare with current one</param>
        /// <returns>true if the current game is equal to the other game, otherwise - false</returns>
        /// <remarks>Equality estimated on team names</remarks>
        public bool Equals(IGame other)
        {
            return other != null &&
                   HomeTeamName == other.HomeTeamName &&
                   AwayTeamName == other.AwayTeamName;
        }

        /// <summary>
        /// Returns hashcode of object based on identical fields
        /// </summary>
        /// <returns>Calculated hashcode</returns>
        public override int GetHashCode()
        {
            return (HomeTeamName + AwayTeamName).GetHashCode();
        }

        /// <summary>
        /// Compares the current game with another game. Returns an integer that indicates whether the current game
        /// precedes, follows, or occurs in the same position in the sort order as the other game
        /// </summary>
        /// <param name="other">A game to compare with current one</param>
        /// <returns>
        /// -1  - current game precedes other one in the sort order
        /// 0   - current game occurs in the same position in the sort order as other one
        /// 1   - current game follows other in the sort order
        /// </returns>
        /// <remarks>Estimation based on total game score and game id value. 
        /// Assuming the id value corresponds the order of adding the game to the system</remarks>
        public int CompareTo(IGame other)
        {
            var thisTotalScore = HomeTeamScore + AwayTeamScore;
            var otherTotalScore = other.HomeTeamScore + other.AwayTeamScore;

            if (thisTotalScore == otherTotalScore)
            {
                return Id > other.Id ? -1 : 1;
            }
            else
            {
                return thisTotalScore > otherTotalScore ? -1 : 1;
            }
        }
        #endregion
    }
}
