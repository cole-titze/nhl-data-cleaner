using Entities.Models;

namespace DataAccess.PlayerRepository
{
    public interface IPlayerRepository
    {
        Task<GameRoster> GetGameRoster(Game game);
    }
}

