using Entities.DbModels;
using Entities.Models;

namespace DataAccess.GameRepository.Mappers
{
	public static class MapDbGameToGame
	{
		public static Game Map(DbGame game)
		{
			return new Game()
			{
                id = game.id,
                homeTeamId = game.homeTeamId,
                awayTeamId = game.awayTeamId,
                seasonStartYear = game.seasonStartYear,
                gameDate = game.gameDate,
                homeGoals = game.homeGoals,
                awayGoals = game.awayGoals,
                winner = game.winner,
                homeSOG = game.homeSOG,
                awaySOG = game.awaySOG,
                homePPG = game.homePPG,
                awayPPG = game.awayPPG,
                homePIM = game.homePIM,
                awayPIM = game.awayPIM,
                homeFaceOffWinPercent = game.homeFaceOffWinPercent,
                awayFaceOffWinPercent = game.awayFaceOffWinPercent,
                homeBlockedShots = game.homeBlockedShots,
                awayBlockedShots = game.awayBlockedShots,
                homeHits = game.homeHits,
                awayHits = game.awayHits,
                homeTakeaways = game.homeTakeaways,
                awayTakeaways = game.awayTakeaways,
                homeGiveaways = game.homeGiveaways,
                awayGiveaways = game.awayGiveaways,
                hasBeenPlayed = game.hasBeenPlayed,
            };
		}
	}
}

