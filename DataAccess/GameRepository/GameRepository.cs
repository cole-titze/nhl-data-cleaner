using DataAccess.GameRepository.Mappers;
using Entities.DbModels;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.GameRepository
{
	public class GameRepository : IGameRepository
	{
        private readonly NhlDbContext _dbContext;
        public GameRepository(NhlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Gets a seasons list of games
        /// </summary>
        /// <param name="seasonStartYear">Year to get games for</param>
        /// <returns>List of season games</returns>
        public async Task<IEnumerable<Game>> GetSeasonGames(int seasonStartYear)
        {
            var dbGames = await GetSeasonRawGames(seasonStartYear);
            var games = new List<Game>();

            foreach(var dbGame in dbGames)
            {
                games.Add(MapDbGameToGame.Map(dbGame));
            }
            return games;
        }
        /// <summary>
        /// Gets season of database model of games
        /// </summary>
        /// <param name="seasonStartYear">Year to get games for</param>
        /// <returns>List of database games</returns>
        private async Task<IEnumerable<DbGame>> GetSeasonRawGames(int seasonStartYear)
        {
            return await _dbContext.Game.Where(x => x.seasonStartYear == seasonStartYear).ToListAsync();
        }
    }
}

