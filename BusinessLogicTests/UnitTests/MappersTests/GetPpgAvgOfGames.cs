using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetPpgAvgOfGames
    {
        [TestClass]
        public class GetGoalsAvgOfGames
        {
            public static IEnumerable<Game> PpgGoalsGameFactory(int homeGameCount, int awayGameCount, int homeGamePpgGoalCount, int awayGamePpgGoalCount, int teamId)
            {
                int otherTeamId = 2;
                var games = new List<Game>();
                int homePpgGoalsPerGame = homeGameCount == 0 ? 0 : homeGamePpgGoalCount / homeGameCount;
                int awayPpgGoalsPerGame = awayGamePpgGoalCount == 0 ? 0 : awayGamePpgGoalCount / awayGameCount;
                int homeInitialPpgGoals = homeGameCount * homePpgGoalsPerGame;
                int awayInitialPpgGoals = awayGameCount * awayPpgGoalsPerGame;

                for (int i = 0; i < homeGameCount; i++)
                {
                    if (i + 1 == homeGameCount && homeInitialPpgGoals != homeGamePpgGoalCount)
                        homePpgGoalsPerGame += homeGamePpgGoalCount - homeInitialPpgGoals;
                    games.Add(new Game()
                    {
                        homeTeamId = teamId,
                        awayTeamId = otherTeamId,
                        homePPG = homePpgGoalsPerGame,
                    });
                }
                for (int i = 0; i < awayGameCount; i++)
                {
                    if (i + 1 == awayGameCount && awayInitialPpgGoals != awayGamePpgGoalCount)
                        awayPpgGoalsPerGame += awayGamePpgGoalCount - awayInitialPpgGoals;
                    games.Add(new Game()
                    {
                        awayTeamId = teamId,
                        homeTeamId = otherTeamId,
                        awayPPG = awayPpgGoalsPerGame,
                    });
                }

                return games;
            }
            [TestMethod]
            public void CallToGetPpgAvgOfGames_WithEmptyGames_ShouldReturnZero()
            {
                int homeGameCount = 0;
                int awayGameCount = 0;
                int homeGamePpgCount = 0;
                int awayGamePpgCount = 0;
                int teamId = 8;
                IEnumerable<Game> teamSeasonGames = PpgGoalsGameFactory(homeGameCount, awayGameCount, homeGamePpgCount, awayGamePpgCount, teamId);
                double expectedPpgAvg = 0;

                var ppgAvg = MapGameToDbCleanedGame.GetPpgAvgOfGames(teamSeasonGames, teamId);

                ppgAvg.Should().Be(expectedPpgAvg);
            }
            [TestMethod]
            public void CallToGetPpgAvgOfGames_WithTenGamesAndTwentyGoals_ShouldReturnTwo()
            {
                int homeGameCount = 3;
                int awayGameCount = 7;
                int homeGamePpgCount = 9;
                int awayGamePpgCount = 11;
                int teamId = 8;
                IEnumerable<Game> teamSeasonGames = PpgGoalsGameFactory(homeGameCount, awayGameCount, homeGamePpgCount, awayGamePpgCount, teamId);
                double expectedPpgAvg = 2;

                var ppgAvg = MapGameToDbCleanedGame.GetPpgAvgOfGames(teamSeasonGames, teamId);

                ppgAvg.Should().Be(expectedPpgAvg);
            }
            [TestMethod]
            public void CallToGetPpgAvgOfGames_WithTenGamesAndFiveGoals_ShouldReturnPointFive()
            {
                int homeGameCount = 7;
                int awayGameCount = 3;
                int homeGamePpgCount = 3;
                int awayGamePpgCount = 2;
                int teamId = 8;
                IEnumerable<Game> teamSeasonGames = PpgGoalsGameFactory(homeGameCount, awayGameCount, homeGamePpgCount, awayGamePpgCount, teamId);
                double expectedPpgAvg = .5;

                var ppgAvg = MapGameToDbCleanedGame.GetPpgAvgOfGames(teamSeasonGames, teamId);

                ppgAvg.Should().Be(expectedPpgAvg);
            }
            [TestMethod]
            public void CallToGetPpgAvgOfGames_WithTenGamesAndZeroGoals_ShouldReturnZero()
            {
                int homeGameCount = 3;
                int awayGameCount = 7;
                int homeGamePpgCount = 0;
                int awayGamePpgCount = 0;
                int teamId = 8;
                IEnumerable<Game> teamSeasonGames = PpgGoalsGameFactory(homeGameCount, awayGameCount, homeGamePpgCount, awayGamePpgCount, teamId);
                double expectedPpgAvg = 0;

                var ppgAvg = MapGameToDbCleanedGame.GetPpgAvgOfGames(teamSeasonGames, teamId);

                ppgAvg.Should().Be(expectedPpgAvg);
            }

        }
    }
}