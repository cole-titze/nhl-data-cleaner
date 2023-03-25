using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class Map
    {
        [TestMethod]
        public void CallToMap_WithEmptyGame_ShouldReturnEmptyGame()
        {
            Game game = new Game();
            SeasonGames seasonGames = new SeasonGames(new List<Game>() { game });
            int expectedId = -1;

            var dbGame = MapGameToDbCleanedGame.Map(game, seasonGames);

            dbGame.gameId.Should().Be(expectedId);
        }
    }
}
