using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Entities.DbModels
{
    // Recent means within last few games ex. 5 games (decided in appsettings)
    public class DbCleanedGame
    {
        [Key]
        public int gameId { get; set; }
        public double homeWinRatio { get; set; }
        public double homeRecentWinRatio { get; set; }
        public double homeRecentGoalsAvg { get; set; }
        public double homeRecentConcededGoalsAvg { get; set; }
        public double homeRecentSogAvg { get; set; }
        public double homeRecentPpgAvg { get; set; }
        public double homeRecentHitsAvg { get; set; }
        public double homeRecentPimAvg { get; set; }
        public double homeRecentBlockedShotsAvg { get; set; }
        public double homeRecentTakeawaysAvg { get; set; }
        public double homeRecentGiveawaysAvg { get; set; }
        public double homeGoalsAvg { get; set; }
        public double homeGoalsAvgAtHome { get; set; }
        public double homeRecentGoalsAvgAtHome { get; set; }
        public double homeConcededGoalsAvg { get; set; }
        public double homeConcededGoalsAvgAtHome { get; set; }
        public double homeRecentConcededGoalsAvgAtHome { get; set; }
        public double homeHoursSinceLastGame { get; set; }
        public double awayWinRatio { get; set; }
        public double awayRecentWinRatio { get; set; }
        public double awayRecentGoalsAvg { get; set; }
        public double awayRecentConcededGoalsAvg { get; set; }
        public double awayRecentSogAvg { get; set; }
        public double awayRecentPpgAvg { get; set; }
        public double awayRecentHitsAvg { get; set; }
        public double awayRecentPimAvg { get; set; }
        public double awayRecentBlockedShotsAvg { get; set; }
        public double awayRecentTakeawaysAvg { get; set; }
        public double awayRecentGiveawaysAvg { get; set; }
        public double awayGoalsAvg { get; set; }
        public double awayGoalsAvgAtAway { get; set; }
        public double awayRecentGoalsAvgAtAway { get; set; }
        public double awayConcededGoalsAvg { get; set; }
        public double awayConcededGoalsAvgAtAway { get; set; }
        public double awayRecentConcededGoalsAvgAtAway { get; set; }
        public double homeRosterOffenseValue { get; set; }
        public double homeRosterDefenseValue { get; set; }
        public double homeRosterGoalieValue { get; set; }
        public double awayRosterOffenseValue { get; set; }
        public double awayRosterDefenseValue { get; set; }
        public double awayRosterGoalieValue { get; set; }
        public double awayHoursSinceLastGame { get; set; }

        [ForeignKey("gameId")]
        public DbGame? game { get; set; } = new DbGame();

        public void Clone(DbCleanedGame game)
        {
            gameId = game.gameId;
            homeWinRatio = game.homeWinRatio;
            homeRecentWinRatio = game.homeRecentWinRatio;
            homeRecentGoalsAvg = game.homeRecentGoalsAvg;
            homeRecentConcededGoalsAvg = game.homeRecentConcededGoalsAvg;
            homeRecentSogAvg = game.homeRecentSogAvg;
            homeRecentPpgAvg = game.homeRecentPpgAvg;
            homeRecentHitsAvg = game.homeRecentHitsAvg;
            homeRecentPimAvg = game.homeRecentPimAvg;
            homeRecentBlockedShotsAvg = game.homeRecentBlockedShotsAvg;
            homeRecentTakeawaysAvg = game.homeRecentTakeawaysAvg;
            homeRecentGiveawaysAvg = game.homeRecentGiveawaysAvg;
            homeGoalsAvg = game.homeGoalsAvg;
            homeGoalsAvgAtHome = game.homeGoalsAvgAtHome;
            homeRecentGoalsAvgAtHome = game.homeRecentGoalsAvgAtHome;
            homeConcededGoalsAvg = game.homeConcededGoalsAvg;
            homeConcededGoalsAvgAtHome = game.homeConcededGoalsAvgAtHome;
            homeRecentConcededGoalsAvgAtHome = game.homeRecentConcededGoalsAvgAtHome;
            homeHoursSinceLastGame = game.homeHoursSinceLastGame;
            awayWinRatio = game.awayWinRatio;
            awayRecentWinRatio = game.awayRecentWinRatio;
            awayRecentGoalsAvg = game.awayRecentGoalsAvg;
            awayRecentConcededGoalsAvg = game.awayRecentConcededGoalsAvg;
            awayRecentSogAvg = game.awayRecentSogAvg;
            awayRecentPpgAvg = game.awayRecentPpgAvg;
            awayRecentHitsAvg = game.awayRecentHitsAvg;
            awayRecentPimAvg = game.awayRecentPimAvg;
            awayRecentBlockedShotsAvg = game.awayRecentBlockedShotsAvg;
            awayRecentTakeawaysAvg = game.awayRecentTakeawaysAvg;
            awayRecentGiveawaysAvg = game.awayRecentGiveawaysAvg;
            awayGoalsAvg = game.awayGoalsAvg;
            awayGoalsAvgAtAway = game.awayGoalsAvgAtAway;
            awayRecentGoalsAvgAtAway = game.awayRecentGoalsAvgAtAway;
            awayConcededGoalsAvg = game.awayConcededGoalsAvg;
            awayConcededGoalsAvgAtAway = game.awayConcededGoalsAvgAtAway;
            awayRecentConcededGoalsAvgAtAway = game.awayRecentConcededGoalsAvgAtAway;
            homeRosterOffenseValue = game.homeRosterOffenseValue;
            homeRosterDefenseValue = game.homeRosterDefenseValue;
            homeRosterGoalieValue = game.homeRosterGoalieValue;
            awayRosterOffenseValue = game.awayRosterOffenseValue;
            awayRosterDefenseValue = game.awayRosterDefenseValue;
            awayRosterGoalieValue = game.awayRosterGoalieValue;
            awayHoursSinceLastGame = game.awayHoursSinceLastGame;
        }
    }
}