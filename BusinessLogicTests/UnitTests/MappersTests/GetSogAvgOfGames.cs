using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetSogAvgOfGames
    {
        public static IEnumerable<Game> SogGameFactory(int homeGameCount, int awayGameCount, int homeSogCount, int awaySogCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeSogPerGame = homeGameCount == 0 ? 0 : homeSogCount / homeGameCount;
            int awaySogPerGame = awaySogCount == 0 ? 0 : awaySogCount / awayGameCount;
            int homeInitialSog = homeGameCount * homeSogPerGame;
            int awayInitialSog = awayGameCount * awaySogPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialSog != homeSogCount)
                    homeSogPerGame += homeSogCount - homeInitialSog;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeSOG = homeSogPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialSog != awaySogCount)
                    awaySogPerGame += awaySogCount - awayInitialSog;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awaySOG = awaySogPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetSogAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameSogCount = 0;
            int awayGameSogCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = SogGameFactory(homeGameCount, awayGameCount, homeGameSogCount, awayGameSogCount, teamId);
            double expectedSogAvg = 0;

            var sogAvg = MapGameToDbCleanedGame.GetSogAvgOfGames(teamSeasonGames, teamId);

            sogAvg.Should().Be(expectedSogAvg);
        }
        [TestMethod]
        public void CallToGetSogAvgOfGames_WithTenGamesAndTwoHundredShots_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameSogCount = 90;
            int awayGameSogCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = SogGameFactory(homeGameCount, awayGameCount, homeGameSogCount, awayGameSogCount, teamId);
            double expectedSogAvg = 20;

            var sogAvg = MapGameToDbCleanedGame.GetSogAvgOfGames(teamSeasonGames, teamId);

            sogAvg.Should().Be(expectedSogAvg);
        }
        [TestMethod]
        public void CallToGetSogAvgOfGames_WithTenGamesAndFiftyShots_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameSogCount = 30;
            int awayGameSogCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = SogGameFactory(homeGameCount, awayGameCount, homeGameSogCount, awayGameSogCount, teamId);
            double expectedSogAvg = 5;

            var sogAvg = MapGameToDbCleanedGame.GetSogAvgOfGames(teamSeasonGames, teamId);

            sogAvg.Should().Be(expectedSogAvg);
        }
        [TestMethod]
        public void CallToGetSogAvgOfGames_WithTenGamesAndZeroShots_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameSogCount = 0;
            int awayGameSogCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = SogGameFactory(homeGameCount, awayGameCount, homeGameSogCount, awayGameSogCount, teamId);
            double expectedSogAvg = 0;

            var sogAvg = MapGameToDbCleanedGame.GetSogAvgOfGames(teamSeasonGames, teamId);

            sogAvg.Should().Be(expectedSogAvg);
        }
    }
}