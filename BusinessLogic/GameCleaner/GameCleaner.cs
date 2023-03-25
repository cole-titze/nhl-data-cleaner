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
        private readonly ILogger<GameCleaner> _logger;
        public GameCleaner(IGameRepository gameRepository, ICleanedGameRepository cleanedGameRepository, IPlayerRepository playerRepo, ILoggerFactory loggerFactory)
        {
            _gameRepo = gameRepository;
            _cleanedGameRepo = cleanedGameRepository;
            _playerRepo = playerRepo;
            _logger = loggerFactory.CreateLogger<GameCleaner>();
        }
        /// <summary>
        /// Gets raw game data from the database and cleans and stores machine learning ready data in the database.
        /// </summary>
        /// <param name="seasonYearRange">The years to run the program for</param>
        /// <returns>None</returns>
        public async Task CleanGamesInSeasons(YearRange seasonYearRange)
		{
            for (int seasonStartYear = seasonYearRange.StartYear; seasonStartYear <= seasonYearRange.EndYear; seasonStartYear++)
            {
                var games = await _gameRepo.GetSeasonGames(seasonStartYear);
                var existingCleanedGames = await _cleanedGameRepo.GetSeasonOfCleanedGames(seasonStartYear);

                var gamesToClean = GetGamesToClean(existingCleanedGames, games);

                if (!gamesToClean.Any())
                {
                    _logger.LogInformation("All game data for season " + seasonStartYear.ToString() + " already exists. Skipping...");
                    continue;
                }
                gamesToClean = await BuildRosters(gamesToClean);
                var cleanedGames = await CleanGames(gamesToClean);

                await _cleanedGameRepo.AddUpdateCleanedGames(cleanedGames);
                await _cleanedGameRepo.Commit();
                _logger.LogInformation("Number of Games Added To Season " + seasonStartYear.ToString() + ": " + cleanedGames.Count().ToString());
            }
        }
        /// <summary>
        /// Adds team rosters to a game object
        /// </summary>
        /// <param name="games"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Game>> BuildRosters(IEnumerable<Game> games)
        {
            foreach(var game in games)
            {
                var gameRoster = await _playerRepo.GetGameRoster(game);
                game.teamRosters = gameRoster;
            }
            return games;
        }
        /// <summary>
        /// Given a list of raw games, these games are cleaned to be ML ready and returned.
        /// </summary>
        /// <param name="gamesToClean">List of raw games</param>
        /// <returns>List of cleaned games</returns>
        private async Task<IEnumerable<DbCleanedGame>> CleanGames(IEnumerable<Game> gamesToClean)
        {
            int seasonStartYear = gamesToClean.First().seasonStartYear;
            var seasonGames = await _gameRepo.GetSeasonGames(seasonStartYear);
            var lastSeasonGames = await _gameRepo.GetSeasonGames(seasonStartYear - 1);
            var gameMap = new SeasonGames(seasonGames.Concat(lastSeasonGames));

            var cleanedGames = new List<DbCleanedGame>();
            foreach(var game in gamesToClean)
            {
                cleanedGames.Add(MapGameToDbCleanedGame.Map(game, gameMap));
            }
            return cleanedGames;
        }
        /// <summary>
        /// Given a list of already cleaned games and list of raw games, returns a list of raw games that need to be cleaned
        /// </summary>
        /// <param name="cleanedGames">cleaned games that already exist</param>
        /// <param name="games">raw games</param>
        /// <returns>Games that need to be cleaned</returns>
        private static IEnumerable<Game> GetGamesToClean(IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var gamesToClean = new List<Game>();
            gamesToClean.AddRange(games.GetFutureGames());
            gamesToClean.AddRange(cleanedGames.GetNewGames(games));

            return gamesToClean.GetUniqueGames();
        }
    }
}

