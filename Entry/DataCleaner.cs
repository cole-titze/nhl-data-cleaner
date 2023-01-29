using System.Diagnostics;
using Microsoft.Extensions.Logging;
using DataAccess;
using DataAccess.GameRepository;
using DataAccess.PlayerRepository;
using Entities.Types;
using BusinessLogic.GameCleaner;
using DataAccess.CleanedGameRepository;

namespace Entry
{
    public class DataCleaner
    {
        private const int START_YEAR = 2010;
        private readonly ILogger<DataCleaner> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public DataCleaner(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<DataCleaner>();
        }
        /// <summary>
        /// Gets and cleans all new games and player values. Stores CleanedGames in db.
        /// </summary>
        /// <param name="gamesConnectionString">db connection string</param>
        /// <returns>None</returns>
        public async Task Main(string gamesConnectionString)
        {
            var watch = Stopwatch.StartNew();
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));

            var nhlDbContext = new NhlDbContext(gamesConnectionString);
            var playerRepo = new PlayerRepository(nhlDbContext);
            var cleanedGameRepo = new CleanedGameRepository(nhlDbContext);
            var gameRepo = new GameRepository(nhlDbContext);
            var yearRange = new YearRange(START_YEAR, DateTime.Now);

            _logger.LogTrace("Starting Game Cleaning");
            var gameCleaner = new GameCleaner(gameRepo, cleanedGameRepo, playerRepo, _loggerFactory);
            await gameCleaner.CleanGamesInSeasons(yearRange);

            watch.Stop();
            var elapsedTime = watch.Elapsed;
            var minutes = elapsedTime.TotalMinutes.ToString();
            _logger.LogTrace("Completed Game Cleaner in " + minutes + " minutes");
        }
    }
}

