using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests.DbModelTests
{
    [TestClass]
    public class DbGamePlayerTests
    {
        [TestMethod]
        public void PlaceholderGamePlayerTest()
        {
            var gamePlayer = new DbGamePlayer()
            {
                gameId = 1,
                teamId = 2,
                playerId = 1129,
                seasonStartYear = 2022,
            };

            gamePlayer.playerId.Should().Be(1129);
        }
    }
}
