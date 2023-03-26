using Entities.DbModels;
using Entities.Models;
using FluentAssertions;

namespace EntitiesTests.UnitTests.ModelsTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void ACallToIsWin_WithAHomeWinAndHomeTeam_ReturnsTrue()
        {
            var teamId = 1;

            var game = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.HOME,
            };

            var isWin = game.IsWin(teamId);

            isWin.Should().Be(true);
        }
        [TestMethod]
        public void ACallToIsWin_WithAnAwayWinAndAwayTeam_ReturnsTrue()
        {
            var teamId = 2;

            var game = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.AWAY,
            };

            var isWin = game.IsWin(teamId);

            isWin.Should().Be(true);
        }
        [TestMethod]
        public void ACallToIsWin_WithAHomeWinAndAwayTeam_ReturnsFalse()
        {
            var teamId = 2;

            var game = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.HOME,
            };

            var isWin = game.IsWin(teamId);

            isWin.Should().Be(false);
        }
        [TestMethod]
        public void ACallToIsWin_WithAnAwayWinAndHomeTeam_ReturnsFalse()
        {
            var teamId = 1;

            var game = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.AWAY,
            };

            var isWin = game.IsWin(teamId);

            isWin.Should().Be(false);
        }
        [TestMethod]
        public void ACallToGetHoursBetweenGames_WithNoPreviousGame_ReturnsDefaultHours()
        {
            int DEFAULT_HOURS = 100;
            Game? firstGame = null;

            var secondGame = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.AWAY,
            };

            var hoursBetween = secondGame.GetHoursBetweenGames(firstGame);

            hoursBetween.Should().Be(DEFAULT_HOURS);
        }
        [TestMethod]
        public void ACallToGetHoursBetweenGames_WithOneDayDifference_ReturnsTwentyFourHours()
        {
            int oneDay = 24;

            Game? firstGame = new Game()
            {
                gameDate = DateTime.Parse("03/25/2023")
            };

            var secondGame = new Game()
            {
                homeTeamId = 1,
                awayTeamId = 2,
                homeFaceOffWinPercent = 33,
                awayFaceOffWinPercent = 67,
                winner = Winner.AWAY,
                gameDate = DateTime.Parse("03/26/2023")
            };

            var hoursBetween = secondGame.GetHoursBetweenGames(firstGame);

            hoursBetween.Should().Be(oneDay);
        }
    }
}
