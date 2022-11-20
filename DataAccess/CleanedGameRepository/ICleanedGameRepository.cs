using Entities.DbModels;

namespace DataAccess.CleanedGameRepository
{
	public interface ICleanedGameRepository
	{
        public Task<IEnumerable<DbCleanedGame>> GetSeasonGames(int seasonStartYear);
        public Task AddCleanedGames(IEnumerable<DbCleanedGame> cleanedGames);
    }
}

