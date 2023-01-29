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
                // Add games to Predicted Game?

                var games = await _gameRepo.GetSeasonGames(seasonStartYear);
                var existingCleanedGames = await _cleanedGameRepo.GetSeasonGames(seasonStartYear);

                var gamesToClean = GetGamesToClean(existingCleanedGames, games);

                if (gamesToClean.Count() == 0)
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
            seasonGames = seasonGames.OrderBy(i => i.gameDate).Reverse().ToList();
            lastSeasonGames = lastSeasonGames.OrderBy(i => i.gameDate).Reverse().ToList();

            var cleanedGames = new List<DbCleanedGame>();
            foreach(var game in gamesToClean)
            {
                cleanedGames.Add(MapGameToDbCleanedGame.Map(game, seasonGames.ToList(), lastSeasonGames.ToList()));
            }
            return cleanedGames;
        }
        /// <summary>
        /// Given a list of already cleaned games and list of raw games, returns a list of raw games that need to be cleaned
        /// </summary>
        /// <param name="cleanedGames">cleaned games that already exist</param>
        /// <param name="games">raw games</param>
        /// <returns>Games that need to be cleaned</returns>
        private IEnumerable<Game> GetGamesToClean(IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var gamesToClean = new List<Game>();
            gamesToClean.AddRange(GetFutureGames(games));
            gamesToClean.AddRange(GetNewGames(cleanedGames, games));

            return GetUniqueGames(gamesToClean);

        }
        /// <summary>
        /// Removes duplicate games
        /// </summary>
        /// <param name="gamesToClean">List of raw games</param>
        /// <returns>A unique list of non-duplicate games</returns>
        private IEnumerable<Game> GetUniqueGames(IEnumerable<Game> gamesToClean)
        {
            return gamesToClean.GroupBy(x => x.id).Select(x => x.First()).ToList();
        }
        /// <summary>
        /// Given existing clean games and list of raw games, returns games that do not currently exist.
        /// </summary>
        /// <param name="cleanedGames">List of already cleaned games</param>
        /// <param name="games">List of raw games</param>
        /// <returns>List of games that did not previously exist</returns>
        private IEnumerable<Game> GetNewGames(IEnumerable<DbCleanedGame> cleanedGames, IEnumerable<Game> games)
        {
            var newGames = new List<Game>();
            cleanedGames = cleanedGames.ToList();

            foreach(var game in games)
            {
                if (cleanedGames.Any(x => x.gameId == game.id))
                    continue;
                newGames.Add(game);
            }

            return newGames;
        }
        /// <summary>
        /// Given a list of games, returns a list of games that have not been played yet.
        /// </summary>
        /// <param name="games">List of games to check</param>
        /// <returns>Games that have not been played yet</returns>
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

