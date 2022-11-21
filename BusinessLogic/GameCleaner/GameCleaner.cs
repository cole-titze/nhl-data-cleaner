using Entities.Types;
using DataAccess.GameRepository;
using DataAccess.PlayerRepository;
using Microsoft.Extensions.Logging;
using DataAccess.CleanedGameRepository;
using Entities.DbModels;
using BusinessLogic.Mappers;
using Entities.Models;

namespace BusinessLogic.GameCleaner
{
	public class GameCleaner
	{
        private readonly IGameRepository _gameRepo;
        private readonly ICleanedGameRepository _cleanedGameRepo;
        private readonly IPlayerRepository _playerRepo;
        private readonly ILogger _logger;
        public GameCleaner(IGameRepository gameRepository, ICleanedGameRepository cleanedGameRepository, IPlayerRepository playerRepo, ILogger logger)
        {
            _gameRepo = gameRepository;
            _cleanedGameRepo = cleanedGameRepository;
            _playerRepo = playerRepo;
            _logger = logger;
        }
        public async Task CleanGamesInSeasons(YearRange seasonYearRange)
		{
            for (int seasonStartYear = seasonYearRange.StartYear; seasonStartYear <= seasonYearRange.EndYear; seasonStartYear++)
            {
                // Add future games to nhl-data-getter
                // Add games to Predicted Game?

                var games = await _gameRepo.GetSeasonGames(seasonStartYear);
                games = await BuildRosters(games);
                var existingCleanedGames = await _cleanedGameRepo.GetSeasonGames(seasonStartYear);

                var gamesToClean = GetGamesToClean(existingCleanedGames, games);

                if (gamesToClean.Count() == 0)
                {
                    _logger.LogInformation("All game data for season " + seasonStartYear.ToString() + " already exists. Skipping...");
                    continue;
                }

                var cleanedGames = await CleanGames(gamesToClean);

                await _cleanedGameRepo.AddCleanedGames(cleanedGames);
                _logger.LogInformation("Number of Games Added To Season" + seasonStartYear.ToString() + ": " + cleanedGames.Count().ToString());
            }
        }

        private async Task<IEnumerable<Game>> BuildRosters(IEnumerable<Game> games)
        {
            foreach(var game in games)
            {
                var gameRoster = await _playerRepo.GetGameRoster(game);
                game.teamRosters = gameRoster;
            }
            return games;
        }

        private async Task<IEnumerable<DbCleanedGame>> CleanGames(IEnumerable<Game> gamesToClean)
        {
            int seasonStartYear = gamesToClean.First().seasonStartYear;
            var seasonGames = await _gameRepo.GetSeasonGames(seasonStartYear);
            var lastSeasonGames = await _gameRepo.GetSeasonGames(seasonStartYear - 1);
            seasonGames = seasonGames.OrderBy(i => i.gameDate).Reverse().ToList();
            lastSeasonGames = lastSeasonGames.OrderBy(i => i.gameDate).Reverse().ToList();

            var cleanedGames = new List<DbCleanedGame>();
            foreach(var game in gamesToClean)
            {
                cleanedGames.Add(MapGameToDbCleanedGame.Map(game, seasonGames.ToList(), lastSeasonGames.ToList()));
            }
            return cleanedGames;
        }

        private IEnumerable<Game> GetGamesToClean(IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var gamesToClean = new List<Game>();
            gamesToClean.AddRange(GetFutureGames(games));
            gamesToClean.AddRange(GetNewGames(cleanedGames, games));

            return GetUniqueGames(gamesToClean);

        }

        private IEnumerable<Game> GetUniqueGames(IEnumerable<Game> gamesToClean)
        {
            return gamesToClean.GroupBy(x => x.id).Select(x => x.First()).ToList();
        }

        private IEnumerable<Game> GetNewGames(IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var newGames = new List<Game>();
            cleanedGames = cleanedGames.ToList();

            foreach(var game in games)
            {
                if (cleanedGames.Any(x => x.id == game.id))
                    continue;
            }

            return newGames;
        }

        private IEnumerable<Game> GetFutureGames(IEnumerable<Game> games)
        {
            var futureGames = new List<Game>();
            foreach(var game in games)
            {
                if (!game.hasBeenPlayed)
                    futureGames.Add(game);
            }

            return futureGames;
        }
    }
}

