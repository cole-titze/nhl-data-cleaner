using Entities.DbModels;
using Entities.Models;
using FluentAssertions;

namespace EntitiesTests.UnitTests.ModelsTests
{
    [TestClass]
    public class PlayerListExtensionsTests
    {
        [TestMethod]
        public void ACallToGetRosterPlayersValue_WithPlayers_ReturnsCorrectPlayerScore()
        {
            var players = new List<DbPlayer>()
            {
                new DbPlayer()
                {
                    value = 112,
                },
                new DbPlayer()
                {
                    value = 141,
                },
            };
            var expectedValue = 253;

            var playerValue = PlayerListExtensions.GetRosterPlayersValue(players);

            playerValue.Should().Be(expectedValue);
        }
    }
}
