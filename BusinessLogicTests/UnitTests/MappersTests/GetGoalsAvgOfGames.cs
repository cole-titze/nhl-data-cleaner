using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetGoalsAvgOfGames
    {
        public static IEnumerable<Game> WinRatioGameFactory(int homeGameCount, int awayGameCount, int homeGameGoalCount, int awayGameGoalCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeGoalsPerGame = homeGameCount == 0 ? 0 : homeGameGoalCount / homeGameCount;
            int awayGoalsPerGame = awayGameGoalCount == 0 ? 0 : awayGameGoalCount / awayGameCount;
            int homeInitialGoals = homeGameCount * homeGoalsPerGame;
            int awayInitialGoals = awayGameCount * awayGoalsPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialGoals != homeGameGoalCount)
                    homeGoalsPerGame += homeGameGoalCount - homeInitialGoals;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    homeGoals = homeGoalsPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialGoals != awayGameGoalCount)
                    awayGoalsPerGame += awayGameGoalCount - awayInitialGoals;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    awayGoals = awayGoalsPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetGoalsAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameGoalCount = 0;
            int awayGameGoalCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameGoalCount, awayGameGoalCount, teamId);
            double expectedGoalAvg = 0;

            var winRatio = MapGameToDbCleanedGame.GetWinRatioOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedGoalAvg);
        }
        [TestMethod]
        public void CallToGetGoalsAvgOfGames_WithTenGamesAndTwentyGoals_ShouldReturnTwo()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameGoalCount = 9;
            int awayGameGoalCount = 11;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameGoalCount, awayGameGoalCount, teamId);
            double expectedGoalAvg = 2;

            var winRatio = MapGameToDbCleanedGame.GetGoalsAvgOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedGoalAvg);
        }
        [TestMethod]
        public void CallToGetGoalsAvgOfGames_WithTenGamesAndZeroGoals_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameGoalCount = 9;
            int awayGameGoalCount = 11;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = WinRatioGameFactory(homeGameCount, awayGameCount, homeGameGoalCount, awayGameGoalCount, teamId);
            double expectedGoalAvg = 2;

            var winRatio = MapGameToDbCleanedGame.GetGoalsAvgOfGames(teamSeasonGames, teamId);

            winRatio.Should().Be(expectedGoalAvg);
        }
    }
}
