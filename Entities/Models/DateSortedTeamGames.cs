using System;
using Entities.DbModels;

namespace Entities.Models
{
	public class DateSortedTeamGames
	{
        public int TeamId { get; set; }
		public readonly IEnumerable<Game> HomeGames;
		public readonly IEnumerable<Game> AwayGames;
		public readonly IEnumerable<Game> Games;
        public readonly IEnumerable<Game> CurrentSeasonGames;

        public DateSortedTeamGames(IEnumerable<Game> games, int teamId)
		{
			TeamId = teamId;
			Games = games.Where(x => x.homeTeamId == teamId || x.awayTeamId == teamId)
				.OrderBy(i => i.gameDate)
				.Reverse()
				.ToList();
			HomeGames = Games.Where(x => x.homeTeamId == teamId).ToList();
			AwayGames = Games.Where(x => x.awayTeamId == teamId).ToList();

            var currentSeason = Games.First().seasonStartYear;
            CurrentSeasonGames = Games.Where(x => x.seasonStartYear == currentSeason).ToList();
        }
    }

    public static class GameListExtensions
    {
        /// <summary>
        /// Gets games that are before the given date.
        /// </summary>
        /// <param name="games">List of games</param>
        /// <param name="date">Date to compare to</param>
        /// <returns>List of games that happened before the given date</returns>
        public static IEnumerable<Game> GetGamesBeforeDate(this IEnumerable<Game> games, DateTime date)
        {
            return games.Where(i => i.gameDate < date).ToList();
        }
        /// <summary>
        /// Removes duplicate games
        /// </summary>
        /// <param name="gamesToClean">List of raw games</param>
        /// <returns>A unique list of non-duplicate games</returns>
        public static IEnumerable<Game> GetUniqueGames(this IEnumerable<Game> gamesToClean)
        {
            return gamesToClean.GroupBy(x => x.id).Select(x => x.First()).ToList();
        }
        /// <summary>
        /// Given existing clean games and list of raw games, returns games that do not currently exist.
        /// </summary>
        /// <param name="cleanedGames">List of already cleaned games</param>
        /// <param name="games">List of raw games</param>
        /// <returns>List of games that did not previously exist</returns>
        public static IEnumerable<Game> GetNewGames(this IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var newGames = new List<Game>();
            cleanedGames = cleanedGames.ToList();

            foreach (var game in games)
            {
                if (cleanedGames.Any(x => x.gameId == game.id))
                    continue;
                newGames.Add(game);
            }

            return newGames;
        }
        /// <summary>
        /// Given a list of games, returns a list of games that have not been played yet.
        /// </summary>
        /// <param name="games">List of games to check</param>
        /// <returns>Games that have not been played yet</returns>
        public static IEnumerable<Game> GetFutureGames(this IEnumerable<Game> games)
        {
            var futureGames = new List<Game>();
            foreach (var game in games)
            {
                if (!game.hasBeenPlayed)
                    futureGames.Add(game);
            }

            return futureGames;
        }

    }
}
