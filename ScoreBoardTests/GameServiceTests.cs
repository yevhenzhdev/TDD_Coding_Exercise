using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using NSubstitute;
using ScoreBoard;
using SBProperties = ScoreBoard.Properties;

namespace ScoreBoardTests
{
    /// <summary>
    /// Tests class <see cref="GameService"/> 
    /// </summary>
    [TestClass]
    public class GameServiceTests
    {
        #region Test data
        private IGame _testGameA;
        private const string _teamA = "Team A";
        private const string _teamAA = "Team AA";

        private IGame _testGameB;
        private const string _teamB = "Team B";
        private const string _teamBB = "Team BB";

        private const string _expectedGamesSummaryByTotalScore =
            "Uruguay 6 - Italy 6\n\rSpain 10 - Brazil 2\n\rMexico 0 - Canada 5\n\rArgentina 3 - Australia 1" +
            "\n\rGermany 2 - France 2\n\r";
        #endregion

        //Games repository substitute
        private IGameRepository _fakeGameRepository;

        //Logger substitute
        private ILogger _fakeLogger;

        //The object to be tested
        private IGameService _gameService;

        [TestInitialize()]
        public void TestInitialize()
        {
            _testGameA = new Game(_teamA, _teamAA);
            _testGameB = new Game(_teamB, _teamBB);

            _fakeGameRepository = Substitute.For<IGameRepository>();
            _fakeLogger = Substitute.For<ILogger>();
            _gameService = new GameService(_fakeGameRepository, _fakeLogger);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            _fakeGameRepository = null;
            _fakeLogger = null;
        }

        #region StartGame method tests
        /// <summary>
        /// Tests that method passes object to the appropriate repository method and doesn't call logger
        /// </summary>
        [TestMethod]
        public void StartGame_CorrectValues_ArgsPassedToRepoLoggerNotCalled()
        {
            //Act
            _gameService.StartGame(_teamA, _teamAA);

            //Assert
            _fakeGameRepository.Received(1).AddGame(Arg.Is<Game>(game => game.Equals(_testGameA)));
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't call repository method with incorrect arguments and calls 
        /// logger with appropriate message
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        [TestMethod]
        [DataRow(null, _teamA, DisplayName = "StartGame_IncorrectValuesNullStr_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamB, null, DisplayName = "StartGame_IncorrectValuesStrNull_RepoNotCalledLoggerMsgReceived")]
        [DataRow(null, null, DisplayName = "StartGame_IncorrectValuesNullNull_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", _teamAA, DisplayName = "StartGame_IncorrectValuesEmptyStr_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamBB, "", DisplayName = "StartGame_IncorrectValuesStrEmpty_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", "", DisplayName = "StartGame_IncorrectValuesEmptyEmpty_RepoNotCalledLoggerMsgReceived")]
        public void StartGame_IncorrectValues_RepoNotCalledLoggerMsgReceived(string homeTeamName, string awayTeamName)
        {
            //Act
            _gameService.StartGame(homeTeamName, awayTeamName);

            //Assert
            _fakeGameRepository.DidNotReceive().AddGame(Arg.Any<IGame>());
            _fakeLogger.Received(1).Log(SBProperties.Resources.ServiceInvalidTeamNamesMsg);
        }
        #endregion

        #region FinishGame method tests
        /// <summary>
        /// Tests that method passes object to the appropriate repository method and doesn't call logger
        /// </summary>
        [TestMethod]
        public void FinishGame_CorrectValues_ArgsPassedToRepoLoggerNotCalled()
        {
            //Act
            _gameService.FinishGame(_teamB, _teamBB);

            //Assert
            _fakeGameRepository.Received(1).RemoveGame(Arg.Is<Game>(game => game.Equals(_testGameB)));
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't call repository method with incorrect arguments and calls 
        /// logger with appropriate message
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        [TestMethod]
        [DataRow(null, _teamA, DisplayName = "FinishGame_IncorrectValuesNullStr_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamB, null, DisplayName = "FinishGame_IncorrectValuesStrNull_RepoNotCalledLoggerMsgReceived")]
        [DataRow(null, null, DisplayName = "FinishGame_IncorrectValuesNullNull_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", _teamAA, DisplayName = "FinishGame_IncorrectValuesEmptyStr_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamBB, "", DisplayName = "FinishGame_IncorrectValuesStrEmpty_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", "", DisplayName = "FinishGame_IncorrectValuesEmptyEmpty_RepoNotCalledLoggerMsgReceived")]
        public void FinishGame_IncorrectValues_RepoNotCalledLoggerMsgReceived(string homeTeamName, string awayTeamName)
        {
            //Act
            _gameService.FinishGame(homeTeamName, awayTeamName);

            //Assert
            _fakeGameRepository.DidNotReceive().RemoveGame(Arg.Any<IGame>());
            _fakeLogger.Received(1).Log(SBProperties.Resources.ServiceInvalidTeamNamesMsg);
        }
        #endregion

        #region UpdateScore method tests
        /// <summary>
        /// Tests that method passes object to the appropriate repository method and doesn't call logger
        /// </summary>
        [TestMethod]
        public void UpdateScore_CorrectValues_ArgsPassedToRepoLoggerNotCalled()
        {
            //Arrange
            _testGameA.HomeTeamScore = 2;
            _testGameA.AwayTeamScore = 3;

            //Act
            _gameService.UpdateScore(_teamA, _testGameA.HomeTeamScore, _teamAA, _testGameA.AwayTeamScore);

            //Assert
            _fakeGameRepository.Received(1).UpdateGameScore(Arg.Is<Game>(game => game.Equals(_testGameA) &&
                                                                                 game.HomeTeamScore == 2 &&
                                                                                 game.AwayTeamScore == 3));
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't call repository method with incorrect arguments and call logger method
        /// with appropriate message
        /// </summary>
        /// <param name="homeTeamScore">Home team score</param>
        /// <param name="awayTeamScore">Away team score</param>
        [TestMethod]
        [DataRow(-1, 2, DisplayName = "UpdateScore_CorrectNamesNegScoreCorrectScore_RepoNotCalledLoggerMsgReceived")]
        [DataRow(3, -4, DisplayName = "UpdateScore_CorrectNamesCorrectScoreNegScore_RepoNotCalledLoggerMsgReceived")]
        [DataRow(-5, -5, DisplayName = "UpdateScore_CorrectNamesNegScoreNegScore_RepoNotCalledLoggerMsgReceived")]
        public void UpdateScore_CorrectNamesIncorrectScores_RepoNotCalledLoggerMsgReceived(int homeTeamScore, int awayTeamScore)
        {
            //Act
            _gameService.UpdateScore(_teamA, homeTeamScore, _teamB, awayTeamScore);

            //Assert
            _fakeGameRepository.DidNotReceive().UpdateGameScore(Arg.Any<IGame>());
            _fakeLogger.Received(1).Log(SBProperties.Resources.ServiceInvalidTeamScoresMsg);
        }

        /// <summary>
        /// Tests that method doesn't call repository method when scores are correct but team names are incorrect, 
        /// but calls logger method with appropriate message
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="awayTeamName">Away team name</param>
        [TestMethod]
        [DataRow(null, _teamA, DisplayName = "UpdateScore_CorrectScoresNullStrNames_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamB, null, DisplayName = "UpdateScore_CorrectScoresStrNamesNull_RepoNotCalledLoggerMsgReceived")]
        [DataRow(null, null, DisplayName = "UpdateScore_CorrectScoresNullNullNames_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", _teamAA, DisplayName = "UpdateScore_CorrectScoresEmptyStrNames_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamBB, "", DisplayName = "UpdateScore_CorrectScoresStrEmptyNames_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", "", DisplayName = "UpdateScore_CorrectScoresEmptyEmptyNames_RepoNotCalledLoggerMsgReceived")]
        public void UpdateScore_CorrectScoresIncorrectNames_RepoNotCalledLoggerMsgReceived(string homeTeamName, string awayTeamName)
        {
            //Act
            _gameService.UpdateScore(homeTeamName, 2, awayTeamName, 3);

            //Assert
            _fakeGameRepository.DidNotReceive().UpdateGameScore(Arg.Any<IGame>());
            _fakeLogger.Received(1).Log(SBProperties.Resources.ServiceInvalidTeamNamesMsg);
        }

        /// <summary>
        /// Tests that method doesn't call repository method with incorrect arguments when different combinations
        /// of incorect/correct argumetns passed and calls logger with appropriate message
        /// </summary>
        /// <param name="homeTeamName">Home team name</param>
        /// <param name="homeTeamScore">Home team score</param>
        /// <param name="awayTeamName">Away team name</param>
        /// <param name="awayTeamScore">Away team score</param>
        [TestMethod]
        [DataRow("", 2, _teamA, -1, DisplayName = "UpdateScore_EmptyNameNegScore_RepoNotCalledLoggerMsgReceived")]
        [DataRow(_teamBB, -3, null, 4, DisplayName = "UpdateScore_NegScoreNulName_RepoNotCalledLoggerMsgReceived")]
        [DataRow("", -1, null, -5, DisplayName = "UpdateScore_NegScoresNullAndEmptyName_RepoNotCalledLoggerMsgReceived")]
        public void UpdateScore_IncorrectNameIncorrectScore_RepoNotCalledLoggerMsgReceived
            (string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
        {
            //Act
            _gameService.UpdateScore(homeTeamName, homeTeamScore, awayTeamName, awayTeamScore);

            //Assert
            _fakeGameRepository.DidNotReceive().UpdateGameScore(Arg.Any<IGame>());
            _fakeLogger.Received(2).Log(Arg.Is<string>(msg => msg == SBProperties.Resources.ServiceInvalidTeamNamesMsg ||
                                                              msg == SBProperties.Resources.ServiceInvalidTeamScoresMsg));
        }
        #endregion

        #region GetGamesSummaryByTotalScore method tests
        /// <summary>
        /// Tests that method calls repository method, but if repository returns empty collection
        /// it calls logger with correct message and returns empty string
        /// </summary>        
        [TestMethod]
        public void GetGamesSummaryByTotalScore_GamesNotAdded_RepoCalledLoggerMsgReceivedReturnedEmpty()
        {
            //Arrange
            _fakeGameRepository.GetCurrentGames().Returns(new List<IGame>());

            //Act
            var result = _gameService.GetGamesSummaryByTotalScore();

            //Assert
            _fakeGameRepository.Received(1).GetCurrentGames();
            _fakeLogger.Received(1).Log(SBProperties.Resources.ServiceNoGamesForSummaryMsg);
            Assert.AreEqual(string.Empty, result);
        }

        /// <summary>
        /// Tests that method calls repository method and if repository returns games collection
        /// the tested method returns correct message with games result
        /// </summary>
        /// <remarks>This test demonstrates satisfaction to the acceptence criteria, defined in task conditions</remarks>
        [TestMethod]
        public void GetGamesSummaryByTotalScore_GamesAdded_ReturnsCorrectResult()
        {
            //Arrange
            var repoGames = GenerateTestGames();
            _fakeGameRepository.GetCurrentGames().Returns(repoGames);

            //Act
            var result = _gameService.GetGamesSummaryByTotalScore();

            //Assert
            _fakeGameRepository.Received(1).GetCurrentGames();
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
            Assert.AreEqual(_expectedGamesSummaryByTotalScore, result);
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Generates test games collection with real data
        /// </summary>
        /// <returns>Games collection</returns>
        private List<IGame> GenerateTestGames()
        {
            var games = new List<IGame>();

            games.Add(new Game("Mexico", "Canada")
            {
                HomeTeamScore = 0,
                AwayTeamScore = 5,
                Id = 1
            });
            games.Add(new Game("Spain", "Brazil")
            {
                HomeTeamScore = 10,
                AwayTeamScore = 2,
                Id = 2
            });
            games.Add(new Game("Germany", "France")
            {
                HomeTeamScore = 2,
                AwayTeamScore = 2,
                Id = 3
            });
            games.Add(new Game("Uruguay", "Italy")
            {
                HomeTeamScore = 6,
                AwayTeamScore = 6,
                Id = 4
            });
            games.Add(new Game("Argentina", "Australia")
            {
                HomeTeamScore = 3,
                AwayTeamScore = 1,
                Id = 5
            });

            return games;
        }
        #endregion
    }
}
