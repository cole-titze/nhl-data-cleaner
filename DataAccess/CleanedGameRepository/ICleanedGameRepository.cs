using Entities.DbModels;

namespace DataAccess.CleanedGameRepository
{
	public interface ICleanedGameRepository
	{
        public Task<IEnumerable<DbCleanedGame>> GetSeasonOfCleanedGames(int seasonStartYear);
        public Task AddUpdateCleanedGames(IEnumerable<DbCleanedGame> cleanedGames);
        public Task Commit();
    }
}

