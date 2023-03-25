using BusinessLogic.Mappers;
using Entities.DbModels;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetWinRatioOfGames
    {
        public static IEnumerable<Game> WinRatioGameFactory(int homeGameCount, int awayGameCount, int homeGameWinCount, int awayGameWinCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            Winner winner = Winner.AWAY;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i < homeGameWinCount)
                    winner = Winner.HOME;
                else
                    winner = Winner.AWAY;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    winner = winner,
                });
            }
            winner = Winner.HOME;
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i < awayGameWinCount)
                    winner = Winner.AWAY;
                else
                    winner = Winner.HOME;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    winner = winner,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetWinRatio_WithEmptyGames_ShouldReturnZero()
        {
            IEnumerable<Game> teamSeasonGames = new List<Game>();
            int teamId = 8;
            double expectedWinRatio = 0;

            var winRatio = MapGameToDbCleanedGame.GetWinRatioOfGames(teamSeasonGames, teamId);
        
            winRatio.Should().Be(expectedWinRatio);
        }
        [TestMethod]
        public void CallToGetWinRatio_WithTenGamesAndHalfWins_ShouldReturnPointFive()
        {
            int homeGameCount = 5;
            int awayGameCount = 5;
            int homeGameWinCount = 2;
            int awayGameWinCount = 3;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameWinCount, awayGameWinCount, teamId);
            double expectedWinRatio = 0.5;

            var winRatio = MapGameToDbCleanedGame.GetWinRatioOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedWinRatio);
        }
        [TestMethod]
        public void CallToGetWinRatio_WithTenGamesAndZeroWins_ShouldReturnZero()
        {
            int homeGameCount = 5;
            int awayGameCount = 5;
            int homeGameWinCount = 0;
            int awayGameWinCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameWinCount, awayGameWinCount, teamId);
            double expectedWinRatio = 0;

            var winRatio = MapGameToDbCleanedGame.GetWinRatioOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedWinRatio);
        }
        [TestMethod]
        public void CallToGetWinRatio_WithTenGamesAndTenWins_ShouldReturnOne()
        {
            int homeGameCount = 5;
            int awayGameCount = 5;
            int homeGameWinCount = 5;
            int awayGameWinCount = 5;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameWinCount, awayGameWinCount, teamId);
            double expectedWinRatio = 1;

            var winRatio = MapGameToDbCleanedGame.GetWinRatioOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedWinRatio);
        }
    }
}
