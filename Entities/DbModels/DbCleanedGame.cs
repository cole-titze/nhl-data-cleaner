namespace Entities.DbModels
{
    // Recent means within last few games ex. 5 games (decided in appsettings)
    public class DbCleanedGame
    {
        public int id { get; set; }
        public int homeTeamId { get; set; }
        public int awayTeamId { get; set; }
        public int seasonStartYear { get; set; }
        public DateTime gameDate { get; set; }
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
        public int winner { get; set; }
        public bool isExcluded { get; set; }
        public bool hasBeenPlayed { get; set; }
    }
}
