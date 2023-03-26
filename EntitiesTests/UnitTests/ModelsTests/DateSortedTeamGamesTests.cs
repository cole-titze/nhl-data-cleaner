using Entities.DbModels;
using Entities.Models;
using FluentAssertions;

namespace EntitiesTests.UnitTests.ModelsTests
{
    [TestClass]
    public class DateSortedTeamGamesTests
    {
        [TestMethod]
        public void ACallToGetNewGames_WithNoNewGames_ReturnsEmptyList()
        {
            var games = new List<Game>()
            {
                new Game()
                {
                    id = 1,
                }
            };
            var cleanedGames = new List<DbCleanedGame>(){
                new DbCleanedGame()
                {
                    gameId = 1,
                }
            };

            var newGames = GameListExtensions.GetNewGames(cleanedGames, games);

            newGames.Count().Should().Be(0);
        }
    }
}
