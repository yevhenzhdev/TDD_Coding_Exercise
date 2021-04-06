using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreBoard
{
    class Game : IGame
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

        public int CompareTo(IGame other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IGame other)
        {
            throw new NotImplementedException();
        }
    }
}
