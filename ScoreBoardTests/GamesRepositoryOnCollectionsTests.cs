using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreBoard;
using NSubstitute;
using SBProperties = ScoreBoard.Properties;

namespace ScoreBoardTests
{
    /// <summary>
    /// Tests class <see cref="GamesRepositoryOnCollectionsTests"/>
    /// </summary>
    [TestClass]
    public class GamesRepositoryOnCollectionsTests
    {
        #region Test data
        private IGame _testGameA;
        private const string _teamA = "Team A";
        private const string _teamAA = "Team AA";

        private IGame _testGameB;
        private const string _teamB = "Team B";
        private const string _teamBB = "Team BB";
        #endregion

        //Logger substitute
        private ILogger _fakeLogger;

        //The object to be tested
        private IGameRepository _gameRepository;

        [TestInitialize()]
        public void TestInitialize()
        {
            _testGameA = new Game(_teamA, _teamAA);
            _testGameB = new Game(_teamB, _teamBB);
            _fakeLogger = Substitute.For<ILogger>();
            _gameRepository = new GamesRepositoryOnCollections(_fakeLogger);
        }

        [TestCleanup()]
        public void TestCleanup()
        {
            _fakeLogger = null;
        }

        #region AddGame method tests
        /// <summary>
        /// Tests that method adds only games passed to it and doesn't call logger
        /// </summary>
        [TestMethod]
        public void AddGame_CorrectArgs_GamesAddedLoggerNotCalled()
        {
            //Act
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(2, allGames.Count);
            Assert.AreEqual(_testGameA, allGames[0]);
            Assert.AreEqual(_testGameB, allGames[1]);
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method adds new games with correct ids and doesn't call logger
        /// </summary>
        [TestMethod]
        public void AddGame_CorrectArgs_GamesAddedWithCorrectIdsLoggerNotCalled()
        {
            //Act
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(2, allGames[1].Id);
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't add games with the same names and scores, 
        /// calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void AddGame_GamesWithSameNamesSameScores_SecondNotAddedLoggerCalledWithMsg()
        {
            //Act
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameA);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.AreEqual(1, allGames[0].Id);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantAddMsg);
        }

        /// <summary>
        ///Tests that method doesn't add games with the same names but different scores,
        ///calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void AddGame_GamesWithSameNamesDiffScores_SecondNotAddedLoggerCalledWithMsg()
        {
            //Arrange
            _testGameA.HomeTeamScore = 2;
            _testGameA.AwayTeamScore = 3;
            _gameRepository.AddGame(_testGameA);

            var theSameGame = new Game(_teamA, _teamAA);

            //Act
            _gameRepository.AddGame(theSameGame);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.AreEqual(2, allGames[0].HomeTeamScore);
            Assert.AreEqual(3, allGames[0].AwayTeamScore);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantAddMsg);
        }

        /// <summary>
        /// Tests that method adds nothing if null passed and calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void AddGame_PassedNull_NothingAddedLoggerCalledWithMsg()
        {
            //Act
            _gameRepository.AddGame(null);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(0, allGames.Count);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantAddMsg);
        }
        #endregion

        #region RemoveGame method tests
        /// <summary>
        /// Tests that method removes only game passed to it and doesn't call logger
        /// </summary>
        [TestMethod]
        public void RemoveGame_CorrectArgs_GameRemoved()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Act
            _gameRepository.RemoveGame(_testGameA);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.IsTrue(_testGameB.Equals(allGames[0]));
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't affect collection if it was passed absent game,
        /// calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void RemoveGame_AbsentGame_GameNotRemovedLoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);

            //Act
            _gameRepository.RemoveGame(_testGameB);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.IsTrue(_testGameA.Equals(allGames[0]));
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantRemoveMsg);
        }

        /// <summary>
        /// Tests that method removes nothing if null passed and calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void RemoveGame_PassedNull_NothingRemovedLoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);

            //Act
            _gameRepository.RemoveGame(null);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.IsTrue(_testGameA.Equals(allGames[0]));
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantRemoveMsg);
        }

        /// <summary>
        /// Tests that method does affect collection if has been added nothing before 
        /// and calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void RemoveGame_CorrectGame_NothingRemovedLoggerCalledWithMsg()
        {
            //Act
            _gameRepository.RemoveGame(_testGameA);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(0, allGames.Count);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantRemoveMsg);
        }
        #endregion

        #region GetCurrentGames method tests
        /// <summary>
        /// Tests that method returns proper collection and doesn't call logger
        /// </summary>
        [TestMethod]
        public void GetCurrentGames_GamesExist_ReturnsGamesCollection()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Act
            var allGames = _gameRepository.GetCurrentGames();

            //Assert after adding
            Assert.AreEqual(2, allGames.Count);
            Assert.IsTrue(_testGameA.Equals(allGames[0]));
            Assert.IsTrue(_testGameB.Equals(allGames[1]));
        }

        /// <summary>
        /// Tests that method returns empty collection if hasn't been added games
        /// and doesn't call logger        
        /// </summary>
        [TestMethod]
        public void GetCurrentGames_GamesNotAdded_ReturnsEmptyCollection()
        {
            //Act
            var allGames = _gameRepository.GetCurrentGames();

            //Assert after adding
            Assert.AreEqual(0, allGames.Count);
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }
        #endregion

        #region UpdateGameScore method tests
        /// <summary>
        /// Tests that method updates score in passed game and doesn't call logger
        /// </summary>
        [TestMethod]
        public void UpdateGameScore_GamesExists_ScoreUpdatedLoggerNotCalled()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);
            var gameWithNewScore = new Game(_teamA, _teamAA)
            {
                HomeTeamScore = 4,
                AwayTeamScore = 5
            };

            //Act
            _gameRepository.UpdateGameScore(gameWithNewScore);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.IsTrue(_testGameA.Equals(allGames[0]));
            Assert.AreEqual(4, allGames[0].HomeTeamScore);
            Assert.AreEqual(5, allGames[0].AwayTeamScore);
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method updates score in passed game and doesn't affect score in another game,
        /// doesn't call logger
        /// </summary>
        [TestMethod]
        public void UpdateGameScore_GamesExists_AnotherGameScoreNotAffectedLoggerNotCalled()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);
            var gameWithNewScore = new Game(_teamB, _teamBB)
            {
                HomeTeamScore = 4,
                AwayTeamScore = 5
            };

            //Act
            _gameRepository.UpdateGameScore(gameWithNewScore);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.IsTrue(_testGameA.Equals(allGames[0]));
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        /// <summary>
        /// Tests that method doesn't affect collection if passed absent game,
        /// calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void UpdateGameScore_GamesNotExists_AnotherGameScoreNotAffectedLoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);

            //Act
            _gameRepository.UpdateGameScore(_testGameB);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(1, allGames.Count);
            Assert.IsTrue(_testGameA.Equals(_gameRepository.GetCurrentGames()[0]));
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantUpdateMsg);
        }

        /// <summary>
        /// Tests that method doesn't affect collection if it has been added nothing before 
        /// and calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void UpdateGameScore_CorrectGame_NothingRemovedLoggerCalledWithMsg()
        {
            //Act
            _gameRepository.UpdateGameScore(_testGameA);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();
            Assert.AreEqual(0, allGames.Count);
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantUpdateMsg);
        }

        /// <summary>
        /// Tests that method doesn't affect collection if passed null 
        /// and calls logger with appropriate message
        /// </summary>
        [TestMethod]
        public void UpdateGameScore_Null_NothingRemovedLoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameB);

            //Act
            _gameRepository.UpdateGameScore(null);

            //Assert
            Assert.IsTrue(_testGameB.Equals(_gameRepository.GetCurrentGames()[0]));
            _fakeLogger.Received(1).Log(SBProperties.Resources.RepositoryCantUpdateMsg);
        }
        #endregion
    }
}
