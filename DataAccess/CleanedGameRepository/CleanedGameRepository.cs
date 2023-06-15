using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CleanedGameRepository
{
	public class CleanedGameRepository : ICleanedGameRepository
	{
        private IEnumerable<DbCleanedGame> _cachedSeasonsGames = new List<DbCleanedGame>();
        private readonly NhlDbContext _dbContext;
        public CleanedGameRepository(NhlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Adds cleaned games if they don't exist. Updates them if they exist
        /// </summary>
        /// <param name="cleanedGames">List of cleaned games to add to database</param>
        /// <returns>None</returns>
        public async Task AddUpdateCleanedGames(IEnumerable<DbCleanedGame> cleanedGames)
        {
            var addList = new List<DbCleanedGame>();
            var updateList = new List<DbCleanedGame>();
            foreach (var game in cleanedGames)
            {
                var dbGame = _cachedSeasonsGames.FirstOrDefault(x => x.gameId == game.gameId);
                game.game = null;
                if (dbGame == null)
                {
                    addList.Add(game);
                }
                else
                {
                    dbGame.Clone(game);
                    updateList.Add(dbGame);
                }
            }
            await _dbContext.CleanedGame.AddRangeAsync(addList);
            _dbContext.CleanedGame.UpdateRange(updateList);
        }

        /// <summary>
        /// Gets a seasons worth of cleaned games and stores them in the cache variable if it doesn't already exist
        /// </summary>
        /// <param name="seasonStartYear">Season start year</param>
        /// <returns>None</returns>
        private async Task CacheSeasonOfCleanedGames(int seasonStartYear)
        {
            var cachedGame = _cachedSeasonsGames.FirstOrDefault();
            if (cachedGame == null || cachedGame.game!.seasonStartYear != seasonStartYear)
                _cachedSeasonsGames = await _dbContext.CleanedGame.Include(x => x.game).Where(x => x.game!.seasonStartYear == seasonStartYear).ToListAsync();
        }
        /// <summary>
        /// Gets a seasons worth of cleaned games
        /// </summary>
        /// <param name="seasonStartYear">Year to get games for</param>
        /// <returns>List of seasons cleaned games</returns>
        public async Task<IEnumerable<DbCleanedGame>> GetSeasonOfCleanedGames(int seasonStartYear)
        {
            await CacheSeasonOfCleanedGames(seasonStartYear);
            return _cachedSeasonsGames;
        }
        /// <summary>
        /// Saves Database changes
        /// </summary>
        /// <returns>None</returns>
        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

