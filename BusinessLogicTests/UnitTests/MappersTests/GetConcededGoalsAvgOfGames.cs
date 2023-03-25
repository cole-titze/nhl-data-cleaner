using BusinessLogic.Mappers;
using Entities.Models;
using FluentAssertions;

namespace BusinessLogicTests.UnitTests.MappersTests
{
    [TestClass]
    public class GetConcededGoalsAvgOfGames
    {
        public static IEnumerable<Game> ConcededGoalsGameFactory(int homeGameCount, int awayGameCount, int homeConcededGoalCount, int awayConcededGoalCount, int teamId)
        {
            int otherTeamId = 2;
            var games = new List<Game>();
            int homeConcededGoalsPerGame = homeGameCount == 0 ? 0 : homeConcededGoalCount / homeGameCount;
            int awayConcededGoalsPerGame = awayConcededGoalCount == 0 ? 0 : awayConcededGoalCount / awayGameCount;
            int homeInitialConcededGoals = homeGameCount * homeConcededGoalsPerGame;
            int awayInitialConcededGoals = awayGameCount * awayConcededGoalsPerGame;

            for (int i = 0; i < homeGameCount; i++)
            {
                if (i + 1 == homeGameCount && homeInitialConcededGoals != homeConcededGoalCount)
                    homeConcededGoalsPerGame += homeConcededGoalCount - homeInitialConcededGoals;
                games.Add(new Game()
                {
                    homeTeamId = teamId,
                    awayTeamId = otherTeamId,
                    awayGoals = homeConcededGoalsPerGame,
                });
            }
            for (int i = 0; i < awayGameCount; i++)
            {
                if (i + 1 == awayGameCount && awayInitialConcededGoals != awayConcededGoalCount)
                    awayConcededGoalsPerGame += awayConcededGoalCount - awayInitialConcededGoals;
                games.Add(new Game()
                {
                    awayTeamId = teamId,
                    homeTeamId = otherTeamId,
                    homeGoals = awayConcededGoalsPerGame,
                });
            }

            return games;
        }
        [TestMethod]
        public void CallToGetConcededGoalsAvgOfGames_WithEmptyGames_ShouldReturnZero()
        {
            int homeGameCount = 0;
            int awayGameCount = 0;
            int homeGameConcededGoalCount = 0;
            int awayGameConcededGoalCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = ConcededGoalsGameFactory(homeGameCount, awayGameCount, homeGameConcededGoalCount, awayGameConcededGoalCount, teamId);
            double expectedSogAvg = 0;

            var concededGoalsAvg = MapGameToDbCleanedGame.GetConcededGoalsAvgOfGames(teamSeasonGames, teamId);

            concededGoalsAvg.Should().Be(expectedSogAvg);
        }
        [TestMethod]
        public void CallToGetConcededGoalsAvgOfGames_WithTenGamesAndTwentyGoalsAgainst_ShouldReturnTw0()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameConcededGoalCount = 9;
            int awayGameConcededGoalCount = 11;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = ConcededGoalsGameFactory(homeGameCount, awayGameCount, homeGameConcededGoalCount, awayGameConcededGoalCount, teamId);
            double expectedConcededGoalsAvg = 2;

            var concededGoalsAvg = MapGameToDbCleanedGame.GetConcededGoalsAvgOfGames(teamSeasonGames, teamId);

            concededGoalsAvg.Should().Be(expectedConcededGoalsAvg);
        }
        [TestMethod]
        public void CallToGetConcededGoalsAvgOfGames_WithTenGamesAndFiveGoals_ShouldReturnPointFive()
        {
            int homeGameCount = 7;
            int awayGameCount = 3;
            int homeGameConcededGoalCount = 3;
            int awayGameConcededGoalCount = 2;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = ConcededGoalsGameFactory(homeGameCount, awayGameCount, homeGameConcededGoalCount, awayGameConcededGoalCount, teamId);
            double expectedConcededGoalsAvg = .5;

            var concededGoalsAvg = MapGameToDbCleanedGame.GetConcededGoalsAvgOfGames(teamSeasonGames, teamId);

            concededGoalsAvg.Should().Be(expectedConcededGoalsAvg);
        }
        [TestMethod]
        public void CallToGetConcededGoalsAvgOfGames_WithTenGamesAndZeroGoals_ShouldReturnZero()
        {
            int homeGameCount = 3;
            int awayGameCount = 7;
            int homeGameConcededGoalCount = 0;
            int awayGameConcededGoalCount = 0;
            int teamId = 8;
            IEnumerable<Game> teamSeasonGames = ConcededGoalsGameFactory(homeGameCount, awayGameCount, homeGameConcededGoalCount, awayGameConcededGoalCount, teamId);
            double expectedConcededGoalsAvg = 0;

            var concededGoalsAvg = MapGameToDbCleanedGame.GetConcededGoalsAvgOfGames(teamSeasonGames, teamId);

            concededGoalsAvg.Should().Be(expectedConcededGoalsAvg);
        }
    }
}