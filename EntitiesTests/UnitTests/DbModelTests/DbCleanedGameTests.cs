using Entities.DbModels;
using FluentAssertions;

namespace EntitiesTests.UnitTests.DbModelTests
{
    [TestClass]
    public class DbCleanedGameTests
    {
        [TestMethod]
        public void ACallToClone_WithAnEmptyGame_DuplicatesTheGame()
        {
            var game = new DbCleanedGame();
            var clonedGame = new DbCleanedGame();

            clonedGame.Clone(game);

            clonedGame.gameId.Should().Be(game.gameId);
            clonedGame.homeWinRatio.Should().Be(game.homeWinRatio);
            clonedGame.homeRecentWinRatio.Should().Be(game.homeRecentWinRatio);
            clonedGame.homeRecentGoalsAvg.Should().Be(game.homeRecentGoalsAvg);
            clonedGame.homeRecentConcededGoalsAvg.Should().Be(game.homeRecentConcededGoalsAvg);
            clonedGame.homeRecentSogAvg.Should().Be(game.homeRecentSogAvg);
            clonedGame.homeRecentPpgAvg.Should().Be(game.homeRecentPpgAvg);
            clonedGame.homeRecentHitsAvg.Should().Be(game.homeRecentHitsAvg);
            clonedGame.homeRecentPimAvg.Should().Be(game.homeRecentPimAvg);
            clonedGame.homeRecentBlockedShotsAvg.Should().Be(game.homeRecentBlockedShotsAvg);
            clonedGame.homeRecentTakeawaysAvg.Should().Be(game.homeRecentTakeawaysAvg);
            clonedGame.homeRecentGiveawaysAvg.Should().Be(game.homeRecentGiveawaysAvg);
            clonedGame.homeGoalsAvg.Should().Be(game.homeGoalsAvg);
            clonedGame.homeGoalsAvgAtHome.Should().Be(game.homeGoalsAvgAtHome);
            clonedGame.homeRecentGoalsAvgAtHome.Should().Be(game.homeRecentGoalsAvgAtHome);
            clonedGame.homeConcededGoalsAvg.Should().Be(game.homeConcededGoalsAvg);
            clonedGame.homeConcededGoalsAvgAtHome.Should().Be(game.homeConcededGoalsAvgAtHome);
            clonedGame.homeRecentConcededGoalsAvgAtHome.Should().Be(game.homeRecentConcededGoalsAvgAtHome);
            clonedGame.homeHoursSinceLastGame.Should().Be(game.homeHoursSinceLastGame);
            clonedGame.awayWinRatio.Should().Be(game.awayWinRatio);
            clonedGame.awayRecentWinRatio.Should().Be(game.awayRecentWinRatio);
            clonedGame.awayRecentGoalsAvg.Should().Be(game.awayRecentGoalsAvg);
            clonedGame.awayRecentConcededGoalsAvg.Should().Be(game.awayRecentConcededGoalsAvg);
            clonedGame.awayRecentSogAvg.Should().Be(game.awayRecentSogAvg);
            clonedGame.awayRecentPpgAvg.Should().Be(game.awayRecentPpgAvg);
            clonedGame.awayRecentHitsAvg.Should().Be(game.awayRecentHitsAvg);
            clonedGame.awayRecentPimAvg.Should().Be(game.awayRecentPimAvg);
            clonedGame.awayRecentBlockedShotsAvg.Should().Be(game.awayRecentBlockedShotsAvg);
            clonedGame.awayRecentTakeawaysAvg.Should().Be(game.awayRecentTakeawaysAvg);
            clonedGame.awayRecentGiveawaysAvg.Should().Be(game.awayRecentGiveawaysAvg);
            clonedGame.awayGoalsAvg.Should().Be(game.awayGoalsAvg);
            clonedGame.awayGoalsAvgAtAway.Should().Be(game.awayGoalsAvgAtAway);
            clonedGame.awayRecentGoalsAvgAtAway.Should().Be(game.awayRecentGoalsAvgAtAway);
            clonedGame.awayConcededGoalsAvg.Should().Be(game.awayConcededGoalsAvg);
            clonedGame.awayConcededGoalsAvgAtAway.Should().Be(game.awayConcededGoalsAvgAtAway);
            clonedGame.awayRecentConcededGoalsAvgAtAway.Should().Be(game.awayRecentConcededGoalsAvgAtAway);
            clonedGame.homeRosterOffenseValue.Should().Be(game.homeRosterOffenseValue);
            clonedGame.homeRosterDefenseValue.Should().Be(game.homeRosterDefenseValue);
            clonedGame.homeRosterGoalieValue.Should().Be(game.homeRosterGoalieValue);
            clonedGame.awayRosterOffenseValue.Should().Be(game.awayRosterOffenseValue);
            clonedGame.awayRosterDefenseValue.Should().Be(game.awayRosterDefenseValue);
            clonedGame.awayRosterGoalieValue.Should().Be(game.awayRosterGoalieValue);
            clonedGame.awayHoursSinceLastGame.Should().Be(game.awayHoursSinceLastGame);
        }
        [TestMethod]
        public void ACallToClone_WithAGame_DuplicatesTheGame()
        {
            var game = new DbCleanedGame()
            {
                gameId = 7,
                homeWinRatio = .3,
                homeRecentWinRatio = .5,
                homeRecentGoalsAvg = 2.45,
                homeRecentConcededGoalsAvg = 4.3,
                homeRecentSogAvg = 33,
                homeRecentPpgAvg = 1.4,
                homeRecentHitsAvg = 23.3,
                homeRecentPimAvg = 23.2,
                homeRecentBlockedShotsAvg = 12.3,
                homeRecentTakeawaysAvg = 13.55,
                homeRecentGiveawaysAvg = 14.66,
                homeGoalsAvg = 2.32,
                homeGoalsAvgAtHome = 1.45,
                homeRecentGoalsAvgAtHome = 2.33,
                homeConcededGoalsAvg = 3.4,
                homeConcededGoalsAvgAtHome = 2.6,
                homeRecentConcededGoalsAvgAtHome = 2.65,
                homeHoursSinceLastGame = 76,
                awayWinRatio = .6,
                awayRecentWinRatio = .55,
                awayRecentGoalsAvg = 2.1,
                awayRecentConcededGoalsAvg = 3.23,
                awayRecentSogAvg = 13.55,
                awayRecentPpgAvg = 2.1,
                awayRecentHitsAvg = 12.3,
                awayRecentPimAvg = 23.5,
                awayRecentBlockedShotsAvg = 12.3,
                awayRecentTakeawaysAvg = 12.33,
                awayRecentGiveawaysAvg = 11.23,
                awayGoalsAvg = 2.1,
                awayGoalsAvgAtAway = 2.4,
                awayRecentGoalsAvgAtAway = 2.3,
                awayConcededGoalsAvg = 4.5,
                awayConcededGoalsAvgAtAway = 4.2,
                awayRecentConcededGoalsAvgAtAway = 2.5,
                homeRosterOffenseValue = 201,
                homeRosterDefenseValue = 213,
                homeRosterGoalieValue = 123,
                awayRosterOffenseValue = 213,
                awayRosterDefenseValue = 210,
                awayRosterGoalieValue = 111,
                awayHoursSinceLastGame = 45,
            };
            var clonedGame = new DbCleanedGame();

            clonedGame.Clone(game);

            clonedGame.gameId.Should().Be(game.gameId);
            clonedGame.homeWinRatio.Should().Be(game.homeWinRatio);
            clonedGame.homeRecentWinRatio.Should().Be(game.homeRecentWinRatio);
            clonedGame.homeRecentGoalsAvg.Should().Be(game.homeRecentGoalsAvg);
            clonedGame.homeRecentConcededGoalsAvg.Should().Be(game.homeRecentConcededGoalsAvg);
            clonedGame.homeRecentSogAvg.Should().Be(game.homeRecentSogAvg);
            clonedGame.homeRecentPpgAvg.Should().Be(game.homeRecentPpgAvg);
            clonedGame.homeRecentHitsAvg.Should().Be(game.homeRecentHitsAvg);
            clonedGame.homeRecentPimAvg.Should().Be(game.homeRecentPimAvg);
            clonedGame.homeRecentBlockedShotsAvg.Should().Be(game.homeRecentBlockedShotsAvg);
            clonedGame.homeRecentTakeawaysAvg.Should().Be(game.homeRecentTakeawaysAvg);
            clonedGame.homeRecentGiveawaysAvg.Should().Be(game.homeRecentGiveawaysAvg);
            clonedGame.homeGoalsAvg.Should().Be(game.homeGoalsAvg);
            clonedGame.homeGoalsAvgAtHome.Should().Be(game.homeGoalsAvgAtHome);
            clonedGame.homeRecentGoalsAvgAtHome.Should().Be(game.homeRecentGoalsAvgAtHome);
            clonedGame.homeConcededGoalsAvg.Should().Be(game.homeConcededGoalsAvg);
            clonedGame.homeConcededGoalsAvgAtHome.Should().Be(game.homeConcededGoalsAvgAtHome);
            clonedGame.homeRecentConcededGoalsAvgAtHome.Should().Be(game.homeRecentConcededGoalsAvgAtHome);
            clonedGame.homeHoursSinceLastGame.Should().Be(game.homeHoursSinceLastGame);
            clonedGame.awayWinRatio.Should().Be(game.awayWinRatio);
            clonedGame.awayRecentWinRatio.Should().Be(game.awayRecentWinRatio);
            clonedGame.awayRecentGoalsAvg.Should().Be(game.awayRecentGoalsAvg);
            clonedGame.awayRecentConcededGoalsAvg.Should().Be(game.awayRecentConcededGoalsAvg);
            clonedGame.awayRecentSogAvg.Should().Be(game.awayRecentSogAvg);
            clonedGame.awayRecentPpgAvg.Should().Be(game.awayRecentPpgAvg);
            clonedGame.awayRecentHitsAvg.Should().Be(game.awayRecentHitsAvg);
            clonedGame.awayRecentPimAvg.Should().Be(game.awayRecentPimAvg);
            clonedGame.awayRecentBlockedShotsAvg.Should().Be(game.awayRecentBlockedShotsAvg);
            clonedGame.awayRecentTakeawaysAvg.Should().Be(game.awayRecentTakeawaysAvg);
            clonedGame.awayRecentGiveawaysAvg.Should().Be(game.awayRecentGiveawaysAvg);
            clonedGame.awayGoalsAvg.Should().Be(game.awayGoalsAvg);
            clonedGame.awayGoalsAvgAtAway.Should().Be(game.awayGoalsAvgAtAway);
            clonedGame.awayRecentGoalsAvgAtAway.Should().Be(game.awayRecentGoalsAvgAtAway);
            clonedGame.awayConcededGoalsAvg.Should().Be(game.awayConcededGoalsAvg);
            clonedGame.awayConcededGoalsAvgAtAway.Should().Be(game.awayConcededGoalsAvgAtAway);
            clonedGame.awayRecentConcededGoalsAvgAtAway.Should().Be(game.awayRecentConcededGoalsAvgAtAway);
            clonedGame.homeRosterOffenseValue.Should().Be(game.homeRosterOffenseValue);
            clonedGame.homeRosterDefenseValue.Should().Be(game.homeRosterDefenseValue);
            clonedGame.homeRosterGoalieValue.Should().Be(game.homeRosterGoalieValue);
            clonedGame.awayRosterOffenseValue.Should().Be(game.awayRosterOffenseValue);
            clonedGame.awayRosterDefenseValue.Should().Be(game.awayRosterDefenseValue);
            clonedGame.awayRosterGoalieValue.Should().Be(game.awayRosterGoalieValue);
            clonedGame.awayHoursSinceLastGame.Should().Be(game.awayHoursSinceLastGame);
        }
    }
}
