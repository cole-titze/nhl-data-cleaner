using Entities.DbModels;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.PlayerRepository
{
	public class PlayerRepository : IPlayerRepository
	{
        private readonly NhlDbContext _dbContext;
        public PlayerRepository(NhlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GameRoster> GetGameRoster(Game game)
        {
            DbPlayer? playerValue;
            var roster = new GameRoster();
            var players = await _dbContext.GamePlayer.Where(x => x.gameId == game.id).ToListAsync();
            foreach(var player in players)
            {
                playerValue = _dbContext.PlayerValue.Where(x => x.seasonStartYear == game.seasonStartYear && x.id == player.playerId).FirstOrDefault();
                if (playerValue == null)
                    continue;

                if(player.teamId == game.homeTeamId)
                {
                    if (playerValue.position == "D")
                        roster.homeDefensePlayers.Add(playerValue);
                    else if (playerValue.position == "G")
                        roster.homeGoalies.Add(playerValue);
                    else
                        roster.homeOffensePlayers.Add(playerValue);
                }
                else
                {
                    if (playerValue.position == "D")
                        roster.awayDefensePlayers.Add(playerValue);
                    else if (playerValue.position == "G")
                        roster.awayGoalies.Add(playerValue);
                    else
                        roster.awayOffensePlayers.Add(playerValue);
                }
            }
            return roster;
        }
    }
}

