namespace ScoreBoard
{
    internal class Game : IGame
    {
        public Game(string homeTeamName, string awayTeamName)
        {
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
        }

        public string HomeTeamName { get; private set; }
        public string AwayTeamName { get; private set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int Id { get; set; }

        public bool Equals(IGame other)
        {
            return other != null &&
                   HomeTeamName == other.HomeTeamName &&
                   AwayTeamName == other.AwayTeamName;
        }

        public override int GetHashCode()
        {
            return (HomeTeamName + AwayTeamName).GetHashCode();
        }

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
    }
}
