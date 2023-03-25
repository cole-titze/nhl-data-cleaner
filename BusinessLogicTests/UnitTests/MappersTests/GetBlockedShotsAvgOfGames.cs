using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetBlockedShotsAvgOfGames
    {
        public static IEnumerable<Game> BlockedShotsGameFactory(int homeGameCount, int awayGameCount, int homeBlockedShotsCount, int awayBlockedShotsCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeBlockedShotsPerGame = homeGameCount == 0 ? 0 : homeBlockedShotsCount / homeGameCount;
            int awayBlockedShotsPerGame = awayBlockedShotsCount == 0 ? 0 : awayBlockedShotsCount / awayGameCount;
            int homeInitialBlockedShots = homeGameCount * homeBlockedShotsPerGame;
            int awayInitialBlockedShots = awayGameCount * awayBlockedShotsPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialBlockedShots != homeBlockedShotsCount)
                    homeBlockedShotsPerGame += homeBlockedShotsCount - homeInitialBlockedShots;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeBlockedShots = homeBlockedShotsPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialBlockedShots != awayBlockedShotsCount)
                    awayBlockedShotsPerGame += awayBlockedShotsCount - awayInitialBlockedShots;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayBlockedShots = awayBlockedShotsPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetBlockedShotsAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameBlockedShotsCount = 0;
            int awayGameBlockedShotsCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = BlockedShotsGameFactory(homeGameCount, awayGameCount, homeGameBlockedShotsCount, awayGameBlockedShotsCount, teamId);
            double expectedBlockedShotsAvg = 0;

            var blockedShotsAvg = MapGameToDbCleanedGame.GetBlockedShotsAvgOfGames(teamSeasonGames, teamId);

            blockedShotsAvg.Should().Be(expectedBlockedShotsAvg);
        }
        [TestMethod]
        public void CallToGetBlockedShotsAvgOfGames_WithTenGamesAndTwoHundredShots_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameBlockedShotsCount = 90;
            int awayGameBlockedShotsCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = BlockedShotsGameFactory(homeGameCount, awayGameCount, homeGameBlockedShotsCount, awayGameBlockedShotsCount, teamId);
            double expectedBlockedShotsAvg = 20;

            var blockedShotsAvg = MapGameToDbCleanedGame.GetBlockedShotsAvgOfGames(teamSeasonGames, teamId);

            blockedShotsAvg.Should().Be(expectedBlockedShotsAvg);
        }
        [TestMethod]
        public void CallToGetBlockedShotsAvgOfGames_WithTenGamesAndFiftyShots_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameBlockedShotsCount = 30;
            int awayGameBlockedShotsCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = BlockedShotsGameFactory(homeGameCount, awayGameCount, homeGameBlockedShotsCount, awayGameBlockedShotsCount, teamId);
            double expectedBlockedShotsAvg = 5;

            var blockedShotsAvg = MapGameToDbCleanedGame.GetBlockedShotsAvgOfGames(teamSeasonGames, teamId);

            blockedShotsAvg.Should().Be(expectedBlockedShotsAvg);
        }
        [TestMethod]
        public void CallToGetBlockedShotsAvgOfGames_WithTenGamesAndZeroShots_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameBlockedShotsCount = 0;
            int awayGameBlockedShotsCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = BlockedShotsGameFactory(homeGameCount, awayGameCount, homeGameBlockedShotsCount, awayGameBlockedShotsCount, teamId);
            double expectedBlockedShotsAvg = 0;

            var blockedShotsAvg = MapGameToDbCleanedGame.GetBlockedShotsAvgOfGames(teamSeasonGames, teamId);

            blockedShotsAvg.Should().Be(expectedBlockedShotsAvg);
        }
    }
}
