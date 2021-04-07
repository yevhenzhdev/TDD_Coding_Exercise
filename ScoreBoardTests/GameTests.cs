using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScoreBoard;

namespace ScoreBoardTests
{
    /// <summary>
    /// Tests class <see cref="Game"/>
    /// </summary>
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void Constructor_FieldsInitializedCorrectly()
        {
            //Act
            var game = new Game("a", "b");

            //Assert
            Assert.AreEqual("a", game.HomeTeamName);
            Assert.AreEqual("b", game.AwayTeamName);
            Assert.AreEqual(0, game.HomeTeamScore);
            Assert.AreEqual(0, game.AwayTeamScore);
            Assert.AreEqual(0, game.Id);
        }

        #region Equals(object obj) method tests
        [TestMethod]
        public void EqualsObj_TeamNamesAreTheSame_ReturnsTrue()
        {
            //Arrange
            var game1 = new Game("A", "B");
            var game2 = new Game("A", "B");

            //Act
            var areEqual = game1.Equals(game2 as object);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void EqualsObj_TeamNamesAreDifferent_ReturnsFalse()
        {
            //Arrange
            var game1 = new Game("A", "b");
            var game2 = new Game("A", "B");

            //Act
            var areEqual = game1.Equals(game2 as object);

            //Assert
            Assert.IsFalse(areEqual);
        }
        #endregion

        #region Equals(IGame other) method tests
        [TestMethod]
        public void Equals_TeamNamesAreTheSame_ReturnsTrue()
        {
            //Arrange
            var game1 = new Game("A", "B");
            var game2 = new Game("A", "B");

            //Act
            var areEqual = game1.Equals(game2);

            //Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Equals_TeamNamesAreDifferent_ReturnsFalse()
        {
            //Arrange
            var game1 = new Game("A", "b");
            var game2 = new Game("A", "B");

            //Act
            var areEqual = game1.Equals(game2);

            //Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void Equals_OtherObjectIsNull_ReturnsFalse()
        {
            //Arrange
            var game1 = new Game("A", "b");

            //Act
            var areEqual = game1.Equals(null);

            //Assert
            Assert.IsFalse(areEqual);
        }
        #endregion

        #region GetHashCode method tests
        [TestMethod]
        public void GetHashCode_ReturnsCorrectValue()
        {
            //Arrange
            var game = new Game("A", "b");
            var namesHash = ("A" + "b").GetHashCode();

            //Act
            var gameHash = game.GetHashCode();

            //Assert
            Assert.AreEqual(namesHash, gameHash);
        }

        [TestMethod]
        public void GetHashCode_SameOnlyTeamNames_ReturnsSameValue()
        {
            //Arrange
            var game1 = new Game("A", "b")
            {
                Id = 100,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            var game2 = new Game("A", "b")
            {
                Id = 200,
                HomeTeamScore = 3,
                AwayTeamScore = 4
            };

            //Act
            var game1Hash = game1.GetHashCode();
            var game2Hash = game2.GetHashCode();

            //Assert
            Assert.AreEqual(game1Hash, game2Hash);
        }

        [TestMethod]
        public void GetHashCode_DiffOnlyTeamNames_ReturnsDiffValue()
        {
            //Arrange
            var game1 = new Game("A", "b")
            {
                Id = 100,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            var game2 = new Game("A", "B")
            {
                Id = 100,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            //Act
            var game1Hash = game1.GetHashCode();
            var game2Hash = game2.GetHashCode();

            //Assert
            Assert.AreNotEqual(game1Hash, game2Hash);
        }
        #endregion

        #region CompareTo method tests
        [TestMethod]
        public void CompareTo_TotalScoresEqual_ReturnsCorrectValues()
        {
            //Arrange
            var game1 = new Game("A", "AA")
            {
                Id = 0,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            var game2 = new Game("B", "BB")
            {
                Id = 1,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            //Act
            var resultGame1 = game1.CompareTo(game2);
            var resultGame2 = game2.CompareTo(game1);

            //Assert
            Assert.AreEqual(1, resultGame1);
            Assert.AreEqual(-1, resultGame2);
        }

        [TestMethod]
        public void CompareTo_TotalScoresDiff_ReturnsCorrectValues()
        {
            //Arrange
            var game1 = new Game("A", "AA")
            {
                Id = 0,
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };

            var game2 = new Game("B", "BB")
            {
                Id = 1,
                HomeTeamScore = 2,
                AwayTeamScore = 2
            };

            //Act
            var resultGame1 = game1.CompareTo(game2);
            var resultGame2 = game2.CompareTo(game1);

            //Assert
            Assert.AreEqual(1, resultGame1);
            Assert.AreEqual(-1, resultGame2);
        }
        #endregion
    }
}
