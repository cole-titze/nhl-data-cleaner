using Entities.DbModels;

namespace Entities.Models
{
	public class GameRoster
	{
        public List<DbPlayer> HomeOffensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> HomeDefensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> HomeGoalies { get; set; } = new List<DbPlayer>();

        public List<DbPlayer> AwayOffensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> AwayDefensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> AwayGoalies { get; set; } = new List<DbPlayer>();


    }
    public static class PlayerListExtensions
    {
        /// <summary>
        /// Given a list of players, returns a total value of the players
        /// </summary>
        /// <param name="players">List of players</param>
        /// <returns>Value score of the players</returns>
        public static double GetRosterPlayersValue(this List<DbPlayer> players)
        {
            double skillScore = 0;
            foreach (var player in players)
            {
                skillScore += player.value;
            }

            return skillScore;
        }
    }
}

