using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreBoard;
using NSubstitute;

namespace ScoreBoardTests
{
    /// <summary>
    /// Tests class <see cref="GamesRepositoryOnCollectionsTests"/>
    /// </summary>
    [TestClass]
    public class GamesRepositoryOnCollectionsTests
    {
        private ILogger _fakeLogger;
        private IGameRepository _gameRepository;

        private IGame _testGameA;
        private const string _teamA = "Team A";
        private const string _teamAA = "Team AA";

        private IGame _testGameB;
        private const string _teamB = "Team B";
        private const string _teamBB = "Team BB";

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

        [TestMethod]
        public void AddGame_CorrectArgs_GamesAddedWithCorrectIds()
        {
            //Act
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();

            Assert.AreEqual(2, allGames.Count);

            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamA, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamAA, allGames[0].AwayTeamName);

            Assert.AreEqual(2, allGames[1].Id);
            Assert.AreEqual(0, allGames[1].HomeTeamScore);
            Assert.AreEqual(0, allGames[1].AwayTeamScore);
            Assert.AreEqual(_teamB, allGames[1].HomeTeamName);
            Assert.AreEqual(_teamBB, allGames[1].AwayTeamName);

            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        [TestMethod]
        public void AddGame_AddTheSameGame_SecondNotAddedLoggerCalledWithMsg()
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

            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(2, allGames[0].HomeTeamScore);
            Assert.AreEqual(3, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamA, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamAA, allGames[0].AwayTeamName);

            _fakeLogger.Received(1).Log("Can't add a new game into repository because it is already exists game or arg is invalid");
        }

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

            Assert.AreEqual(2, allGames[0].Id);
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamB, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamBB, allGames[0].AwayTeamName);

            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        [TestMethod]
        public void RemoveGame_IncorrectArgs_GameNotRemovedLoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);

            //Act
            _gameRepository.RemoveGame(_testGameB);
            _gameRepository.RemoveGame(null);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();

            Assert.AreEqual(1, allGames.Count);

            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamA, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamAA, allGames[0].AwayTeamName);

            _fakeLogger.Received(2).Log("Can't remove the game from repository because there is no such game or arg is invalid");
        }

        [TestMethod]
        public void GetCurrentGames_GamesExist_ReturnsGamesCollection()
        {
            //Assert before adding
            Assert.AreEqual(0, _gameRepository.GetCurrentGames().Count);

            //Arrange
            _gameRepository.AddGame(_testGameA);
            _gameRepository.AddGame(_testGameB);

            //Act
            var result = _gameRepository.GetCurrentGames();

            //Assert after adding
            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(0, result[0].HomeTeamScore);
            Assert.AreEqual(0, result[0].AwayTeamScore);
            Assert.AreEqual(_teamA, result[0].HomeTeamName);
            Assert.AreEqual(_teamAA, result[0].AwayTeamName);

            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(0, result[1].HomeTeamScore);
            Assert.AreEqual(0, result[1].AwayTeamScore);
            Assert.AreEqual(_teamB, result[1].HomeTeamName);
            Assert.AreEqual(_teamBB, result[1].AwayTeamName);

            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

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

            Assert.AreEqual(1, allGames.Count);

            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(4, allGames[0].HomeTeamScore);
            Assert.AreEqual(5, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamA, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamAA, allGames[0].AwayTeamName);

            _fakeLogger.DidNotReceive().Log(Arg.Any<string>());
        }

        [TestMethod]
        public void UpdateGameScore_GamesNotExists_LoggerCalledWithMsg()
        {
            //Arrange
            _gameRepository.AddGame(_testGameA);
            var gameWithNewScore = new Game(_teamB, _teamAA)
            {
                HomeTeamScore = 4,
                AwayTeamScore = 5
            };

            //Act
            _gameRepository.UpdateGameScore(gameWithNewScore);

            //Assert
            var allGames = _gameRepository.GetCurrentGames();

            Assert.AreEqual(1, allGames.Count);

            Assert.AreEqual(1, allGames[0].Id);
            Assert.AreEqual(0, allGames[0].HomeTeamScore);
            Assert.AreEqual(0, allGames[0].AwayTeamScore);
            Assert.AreEqual(_teamA, allGames[0].HomeTeamName);
            Assert.AreEqual(_teamAA, allGames[0].AwayTeamName);

            _fakeLogger.Received(1).Log("Can't update the game score in repository because there is no such game or arg is invalid");
        }
    }
}
