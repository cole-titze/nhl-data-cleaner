using Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.CleanedGameRepository
{
	public class CleanedGameRepository : ICleanedGameRepository
	{
        private readonly NhlDbContext _dbContext;
        public CleanedGameRepository(NhlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCleanedGames(IEnumerable<DbCleanedGame> cleanedGames)
        {
            await _dbContext.CleanedGame.AddRangeAsync(cleanedGames);
        }

        public async Task<IEnumerable<DbCleanedGame>> GetSeasonGames(int seasonStartYear)
        {
            return await _dbContext.CleanedGame.Where(x => x.seasonStartYear == seasonStartYear).ToListAsync();
        }
    }
}

