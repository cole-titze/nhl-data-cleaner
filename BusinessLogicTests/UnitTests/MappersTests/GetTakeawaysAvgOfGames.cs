using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetTakeawaysAvgOfGames
    {
        public static IEnumerable<Game> TakeawaysGameFactory(int homeGameCount, int awayGameCount, int homeTakeawaysCount, int awayTakeawaysCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeTakeawaysPerGame = homeGameCount == 0 ? 0 : homeTakeawaysCount / homeGameCount;
            int awayTakeawaysPerGame = awayTakeawaysCount == 0 ? 0 : awayTakeawaysCount / awayGameCount;
            int homeInitialTakeaways = homeGameCount * homeTakeawaysPerGame;
            int awayInitialTakeaways = awayGameCount * awayTakeawaysPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialTakeaways != homeTakeawaysCount)
                    homeTakeawaysPerGame += homeTakeawaysCount - homeInitialTakeaways;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeTakeaways = homeTakeawaysPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialTakeaways != awayTakeawaysCount)
                    awayTakeawaysPerGame += awayTakeawaysCount - awayInitialTakeaways;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayTakeaways = awayTakeawaysPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetTakeawaysAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameTakeawayCount = 0;
            int awayGameTakeawayCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = TakeawaysGameFactory(homeGameCount, awayGameCount, homeGameTakeawayCount, awayGameTakeawayCount, teamId);
            double expectedTakeawayAvg = 0;

            var takeawayAvg = MapGameToDbCleanedGame.GetTakeawaysAvgOfGames(teamSeasonGames, teamId);

            takeawayAvg.Should().Be(expectedTakeawayAvg);
        }
        [TestMethod]
        public void CallToGetTakeawaysAvgOfGames_WithTenGamesAndTwoHundredTakeaways_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameTakeawayCount = 90;
            int awayGameTakeawayCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = TakeawaysGameFactory(homeGameCount, awayGameCount, homeGameTakeawayCount, awayGameTakeawayCount, teamId);
            double expectedTakeawayAvg = 20;

            var takeawayAvg = MapGameToDbCleanedGame.GetTakeawaysAvgOfGames(teamSeasonGames, teamId);

            takeawayAvg.Should().Be(expectedTakeawayAvg);
        }
        [TestMethod]
        public void CallToGetTakeawaysAvgOfGames_WithTenGamesAndFiftyTakeaways_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameTakeawayCount = 30;
            int awayGameTakeawayCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = TakeawaysGameFactory(homeGameCount, awayGameCount, homeGameTakeawayCount, awayGameTakeawayCount, teamId);
            double expectedTakeawayAvg = 5;

            var takeawayAvg = MapGameToDbCleanedGame.GetTakeawaysAvgOfGames(teamSeasonGames, teamId);

            takeawayAvg.Should().Be(expectedTakeawayAvg);
        }
        [TestMethod]
        public void CallToGetTakeawaysAvgOfGames_WithTenGamesAndZeroTakeaways_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameTakeawayCount = 0;
            int awayGameTakeawayCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = TakeawaysGameFactory(homeGameCount, awayGameCount, homeGameTakeawayCount, awayGameTakeawayCount, teamId);
            double expectedTakeawayAvg = 0;

            var takeawayAvg = MapGameToDbCleanedGame.GetTakeawaysAvgOfGames(teamSeasonGames, teamId);

            takeawayAvg.Should().Be(expectedTakeawayAvg);
        }
    }
}
