namespace Entities.Models
{
    public class SeasonGames
	{
		public readonly IDictionary<int, DateSortedTeamGames> GamesMap = new Dictionary<int, DateSortedTeamGames>();
		public SeasonGames(IEnumerable<Game> games)
		{
			foreach(var game in games)
			{
				GamesMap.TryAdd(game.homeTeamId, new DateSortedTeamGames(games, game.homeTeamId));
                GamesMap.TryAdd(game.awayTeamId, new DateSortedTeamGames(games, game.awayTeamId));
            }
        }
	}
}

