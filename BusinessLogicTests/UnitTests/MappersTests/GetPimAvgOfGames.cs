using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetPimAvgOfGames
    {
        public static IEnumerable<Game> PimGameFactory(int homeGameCount, int awayGameCount, int homePimCount, int awayPimCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homePimPerGame = homeGameCount == 0 ? 0 : homePimCount / homeGameCount;
            int awayPimPerGame = awayPimCount == 0 ? 0 : awayPimCount / awayGameCount;
            int homeInitialPim = homeGameCount * homePimPerGame;
            int awayInitialPim = awayGameCount * awayPimPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialPim != homePimCount)
                    homePimPerGame += homePimCount - homeInitialPim;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homePIM = homePimPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialPim != awayPimCount)
                    awayPimPerGame += awayPimCount - awayInitialPim;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayPIM = awayPimPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetPimAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGamePimCount = 0;
            int awayGamePimCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = PimGameFactory(homeGameCount, awayGameCount, homeGamePimCount, awayGamePimCount, teamId);
            double expectedPimAvg = 0;

            var PimAvg = MapGameToDbCleanedGame.GetPimAvgOfGames(teamSeasonGames, teamId);

            PimAvg.Should().Be(expectedPimAvg);
        }
        [TestMethod]
        public void CallToGetPimAvgOfGames_WithTenGamesAndTwoHundredMinutes_ShouldReturnTwenty()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGamePimCount = 90;
            int awayGamePimCount = 110;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = PimGameFactory(homeGameCount, awayGameCount, homeGamePimCount, awayGamePimCount, teamId);
            double expectedPimAvg = 20;

            var PimAvg = MapGameToDbCleanedGame.GetPimAvgOfGames(teamSeasonGames, teamId);

            PimAvg.Should().Be(expectedPimAvg);
        }
        [TestMethod]
        public void CallToGetPimAvgOfGames_WithTenGamesAndFiftyMinutes_ShouldReturnFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGamePimCount = 30;
            int awayGamePimCount = 20;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = PimGameFactory(homeGameCount, awayGameCount, homeGamePimCount, awayGamePimCount, teamId);
            double expectedPimAvg = 5;

            var PimAvg = MapGameToDbCleanedGame.GetPimAvgOfGames(teamSeasonGames, teamId);

            PimAvg.Should().Be(expectedPimAvg);
        }
        [TestMethod]
        public void CallToGetPimAvgOfGames_WithTenGamesAndZeroMinutes_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGamePimCount = 0;
            int awayGamePimCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = PimGameFactory(homeGameCount, awayGameCount, homeGamePimCount, awayGamePimCount, teamId);
            double expectedPimAvg = 0;

            var PimAvg = MapGameToDbCleanedGame.GetPimAvgOfGames(teamSeasonGames, teamId);

            PimAvg.Should().Be(expectedPimAvg);
        }
    }
}
