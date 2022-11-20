using Entities.Models;

namespace DataAccess.GameRepository
{
	public interface IGameRepository
	{
        public Task<IEnumerable<Game>> GetSeasonGames(int seasonStartYear);
	}
}

