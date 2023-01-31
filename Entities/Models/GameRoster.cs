using Entities.DbModels;

namespace Entities.Models
{
	public class GameRoster
	{
        public List<DbPlayer> homeOffensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> homeDefensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> homeGoalies { get; set; } = new List<DbPlayer>();

        public List<DbPlayer> awayOffensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> awayDefensePlayers { get; set; } = new List<DbPlayer>();
        public List<DbPlayer> awayGoalies { get; set; } = new List<DbPlayer>();


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

