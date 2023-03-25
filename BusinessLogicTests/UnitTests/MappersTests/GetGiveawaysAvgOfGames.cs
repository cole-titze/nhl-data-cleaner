using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetGiveawaysAvgOfGames
    {
        public static IEnumerable<Game> GiveawaysGameFactory(int homeGameCount, int awayGameCount, int homeGiveawaysCount, int awayGiveawaysCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeGiveawaysPerGame = homeGameCount == 0 ? 0 : homeGiveawaysCount / homeGameCount;
            int awayGiveawaysPerGame = awayGiveawaysCount == 0 ? 0 : awayGiveawaysCount / awayGameCount;
            int homeInitialGiveaways = homeGameCount * homeGiveawaysPerGame;
            int awayInitialGiveaways = awayGameCount * awayGiveawaysPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialGiveaways != homeGiveawaysCount)
                    homeGiveawaysPerGame += homeGiveawaysCount - homeInitialGiveaways;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeGiveaways = homeGiveawaysPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialGiveaways != awayGiveawaysCount)
                    awayGiveawaysPerGame += awayGiveawaysCount - awayInitialGiveaways;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayGiveaways = awayGiveawaysPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetGiveawaysAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameGiveawayCount = 0;
            int awayGameGiveawayCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = GiveawaysGameFactory(homeGameCount, awayGameCount, homeGameGiveawayCount, awayGameGiveawayCount, teamId);
            double expectedGiveawayAvg = 0;

            var GiveawayAvg = MapGameToDbCleanedGame.GetGiveawaysAvgOfGames(teamSeasonGames, teamId);

            GiveawayAvg.Should().Be(expectedGiveawayAvg);
        }
        [TestMethod]
        public void CallToGetGiveawaysAvgOfGames_WithTenGamesAndTwoHundredGiveaways_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameGiveawayCount = 90;
            int awayGameGiveawayCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = GiveawaysGameFactory(homeGameCount, awayGameCount, homeGameGiveawayCount, awayGameGiveawayCount, teamId);
            double expectedGiveawayAvg = 20;

            var GiveawayAvg = MapGameToDbCleanedGame.GetGiveawaysAvgOfGames(teamSeasonGames, teamId);

            GiveawayAvg.Should().Be(expectedGiveawayAvg);
        }
        [TestMethod]
        public void CallToGetGiveawaysAvgOfGames_WithTenGamesAndFiftyGiveaways_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameGiveawayCount = 30;
            int awayGameGiveawayCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = GiveawaysGameFactory(homeGameCount, awayGameCount, homeGameGiveawayCount, awayGameGiveawayCount, teamId);
            double expectedGiveawayAvg = 5;

            var GiveawayAvg = MapGameToDbCleanedGame.GetGiveawaysAvgOfGames(teamSeasonGames, teamId);

            GiveawayAvg.Should().Be(expectedGiveawayAvg);
        }
        [TestMethod]
        public void CallToGetGiveawaysAvgOfGames_WithTenGamesAndZeroGiveaways_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameGiveawayCount = 0;
            int awayGameGiveawayCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = GiveawaysGameFactory(homeGameCount, awayGameCount, homeGameGiveawayCount, awayGameGiveawayCount, teamId);
            double expectedGiveawayAvg = 0;

            var GiveawayAvg = MapGameToDbCleanedGame.GetGiveawaysAvgOfGames(teamSeasonGames, teamId);

            GiveawayAvg.Should().Be(expectedGiveawayAvg);
        }
    }
}
