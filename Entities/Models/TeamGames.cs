using System;
namespace Entities.Models
{
	public class TeamGames
	{
        public int TeamId { get; set; }
		public readonly IEnumerable<Game> HomeGames;
		public readonly IEnumerable<Game> AwayGames;
		public readonly IEnumerable<Game> Games;

		public TeamGames(IEnumerable<Game> games, int teamId)
		{
			TeamId = teamId;
			Games = games.Where(x => x.homeTeamId == teamId || x.awayTeamId == teamId)
				.OrderBy(i => i.gameDate)
				.Reverse()
				.ToList();
			HomeGames = Games.Where(x => x.homeTeamId == teamId).ToList();
			AwayGames = Games.Where(x => x.awayTeamId == teamId).ToList();
        }
    }

    public static class GameListExtensions
    {
        public static IEnumerable<Game> GetGamesBeforeDate(this IEnumerable<Game> games, DateTime date)
        {
            return games.Where(i => i.gameDate < date).ToList();
        }
    }
}
