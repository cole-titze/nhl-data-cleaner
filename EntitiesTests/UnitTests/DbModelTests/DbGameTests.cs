using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests.DbModelTests
{
    [TestClass]
    public class DbGameTests
    {
        [TestMethod]
        public void ACallToIsValid_WithAnInvalidGame_ReturnsFalse()
        {
            var invalidGame = new DbGame()
            {
                homeFaceOffWinPercent = 0,
                awayFaceOffWinPercent = 0,
            };

            var isGameValid = invalidGame.IsValid();

            isGameValid.Should().Be(false);
        }
        [TestMethod]
        public void ACallToIsValid_WithAValidGame_ReturnsFalse()
        {
            var invalidGame = new DbGame()
            {
                homeTeamId = 3,
                awayTeamId = 8,
                seasonStartYear = 2021,
                gameDate = DateTime.Parse("03/25/2023"),
                homeGoals = 4,
                awayGoals = 5,
                winner = Winner.AWAY,
                homeSOG = 33,
                awaySOG = 32,
                homePPG = 1,
                awayPPG = 0,
                homePIM = 6,
                awayPIM = 4,
                homeFaceOffWinPercent = 55.25,
                awayFaceOffWinPercent = 44.75,
                homeBlockedShots = 10,
                awayBlockedShots = 7,
                homeHits = 21,
                awayHits = 12,
                homeTakeaways = 11,
                awayTakeaways = 12,
                homeGiveaways = 12,
                awayGiveaways = 11,
                hasBeenPlayed = false,
            };

            var isGameValid = invalidGame.IsValid();

            isGameValid.Should().Be(true);
        }
    }
}
