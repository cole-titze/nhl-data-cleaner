using Entities.DbModels;

namespace Entities.Models
{
    public class Game
    {
        // If no game has been played set default as 4 days of rest (season hasn't started)
        public static readonly int DEFAULT_HOURS = 100;

        public int id { get; set; } = -1;
        public int homeTeamId { get; set; }
        public int awayTeamId { get; set; }
        public int seasonStartYear { get; set; }
        public DateTime gameDate { get; set; }
        public int homeGoals { get; set; }
        public int awayGoals { get; set; }
        public Winner winner { get; set; }
        public int homeSOG { get; set; }
        public int awaySOG { get; set; }
        public int homePPG { get; set; }
        public int awayPPG { get; set; }
        public int homePIM { get; set; }
        public int awayPIM { get; set; }
        public double homeFaceOffWinPercent { get; set; }
        public double awayFaceOffWinPercent { get; set; }
        public int homeBlockedShots { get; set; }
        public int awayBlockedShots { get; set; }
        public int homeHits { get; set; }
        public int awayHits { get; set; }
        public int homeTakeaways { get; set; }
        public int awayTakeaways { get; set; }
        public int homeGiveaways { get; set; }
        public int awayGiveaways { get; set; }
        public bool hasBeenPlayed { get; set; }

        public GameRoster teamRosters { get; set; } = new GameRoster();

        /// <summary>
        /// Gets whether a game is valid or not
        /// </summary>
        /// <returns>True if both teams won 0 faceoffs</returns>
        public bool IsValid()
        {
            return homeFaceOffWinPercent != 0 || awayFaceOffWinPercent != 0;
        }
        /// <summary>
        /// Gets if the team won or not
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public bool IsWin(int teamId)
        {
            if (homeTeamId == teamId && winner == Winner.HOME) return true;
            if (awayTeamId == teamId && winner == Winner.AWAY) return true;
            return false;
        }
        /// <summary>
        /// Gets the hours between the current game and game passed in
        /// </summary>
        /// <param name="game">Game to find hours between.</param>
        /// <returns></returns>
        public double GetHoursBetweenGames(Game? game)
        {
            if (game == null)
                return DEFAULT_HOURS;
            var hourDifference = (gameDate - game.gameDate).TotalHours;

            return Math.Abs(hourDifference);
        }
    }
}
