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
        /// <summary>
        /// Gets the game roster for a given game
        /// </summary>
        /// <param name="game">Game to get roster for</param>
        /// <returns>Game roster</returns>
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
                        roster.HomeDefensePlayers.Add(playerValue);
                    else if (playerValue.position == "G")
                        roster.HomeGoalies.Add(playerValue);
                    else
                        roster.HomeOffensePlayers.Add(playerValue);
                }
                else
                {
                    if (playerValue.position == "D")
                        roster.AwayDefensePlayers.Add(playerValue);
                    else if (playerValue.position == "G")
                        roster.AwayGoalies.Add(playerValue);
                    else
                        roster.AwayOffensePlayers.Add(playerValue);
                }
            }
            return roster;
        }
    }
}

