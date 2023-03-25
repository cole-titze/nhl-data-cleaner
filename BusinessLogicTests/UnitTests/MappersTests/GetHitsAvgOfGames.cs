using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetHitsAvgOfGames
    {
        public static IEnumerable<Game> HitsGameFactory(int homeGameCount, int awayGameCount, int homeHitsCount, int awayHitsCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeHitsPerGame = homeGameCount == 0 ? 0 : homeHitsCount / homeGameCount;
            int awayHitsPerGame = awayHitsCount == 0 ? 0 : awayHitsCount / awayGameCount;
            int homeInitialHits = homeGameCount * homeHitsPerGame;
            int awayInitialHits = awayGameCount * awayHitsPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialHits != homeHitsCount)
                    homeHitsPerGame += homeHitsCount - homeInitialHits;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeHits = homeHitsPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialHits != awayHitsCount)
                    awayHitsPerGame += awayHitsCount - awayInitialHits;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayHits = awayHitsPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetHitsAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameHitsCount = 0;
            int awayGameHitsCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = HitsGameFactory(homeGameCount, awayGameCount, homeGameHitsCount, awayGameHitsCount, teamId);
            double expectedHitsAvg = 0;

            var hitsAvg = MapGameToDbCleanedGame.GetHitsAvgOfGames(teamSeasonGames, teamId);

            hitsAvg.Should().Be(expectedHitsAvg);
        }
        [TestMethod]
        public void CallToGetHitsAvgOfGames_WithTenGamesAndTwoHundredHits_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameHitsCount = 90;
            int awayGameHitsCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = HitsGameFactory(homeGameCount, awayGameCount, homeGameHitsCount, awayGameHitsCount, teamId);
            double expectedHitsAvg = 20;

            var hitsAvg = MapGameToDbCleanedGame.GetHitsAvgOfGames(teamSeasonGames, teamId);

            hitsAvg.Should().Be(expectedHitsAvg);
        }
        [TestMethod]
        public void CallToGetHitsAvgOfGames_WithTenGamesAndFiftyHits_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameHitsCount = 30;
            int awayGameHitsCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = HitsGameFactory(homeGameCount, awayGameCount, homeGameHitsCount, awayGameHitsCount, teamId);
            double expectedHitsAvg = 5;

            var hitsAvg = MapGameToDbCleanedGame.GetHitsAvgOfGames(teamSeasonGames, teamId);

            hitsAvg.Should().Be(expectedHitsAvg);
        }
        [TestMethod]
        public void CallToGetHitsAvgOfGames_WithTenGamesAndZeroHits_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameHitsCount = 0;
            int awayGameHitsCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = HitsGameFactory(homeGameCount, awayGameCount, homeGameHitsCount, awayGameHitsCount, teamId);
            double expectedHitsAvg = 0;

            var hitsAvg = MapGameToDbCleanedGame.GetHitsAvgOfGames(teamSeasonGames, teamId);

            hitsAvg.Should().Be(expectedHitsAvg);
        }
    }
}
