using System;
namespace Entities.Models
{
	public class SeasonGames
	{
		public readonly IDictionary<int, TeamGames> GamesMap = new Dictionary<int, TeamGames>();
		public SeasonGames(IEnumerable<Game> games)
		{
			foreach(var game in games)
			{
				GamesMap.TryAdd(game.homeTeamId, new TeamGames(games, game.homeTeamId));
                GamesMap.TryAdd(game.awayTeamId, new TeamGames(games, game.awayTeamId));
            }
        }
	}
}

