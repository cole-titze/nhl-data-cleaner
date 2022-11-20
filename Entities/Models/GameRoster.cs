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
}

