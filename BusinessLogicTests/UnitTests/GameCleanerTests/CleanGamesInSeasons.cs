using Entities.Models;
using FakeItEasy;
using DataAccess.PlayerRepository;
using DataAccess.GameRepository;
using DataAccess.CleanedGameRepository;
using Microsoft.Extensions.Logging;
using BusinessLogic.GameCleaner;
using Entities.DbModels;
using Entities.Types;

namespace BusinessLogicTests.UnitTests.GameCleanerTests
{
    [TestClass]
    public class CleanGamesInSeasons
    {
        private YearRange mockYearRange = new YearRange(2020, 2022);

        [TestMethod]
        public async Task CallToCleanGamesInSeasons_WithEmptyGames_ShouldNotCallCommit()
        {
            var gameRepo = A.Fake<IGameRepository>();
            var cleanedGameRepo = A.Fake<ICleanedGameRepository>();
            var playerRepo = A.Fake<IPlayerRepository>();
            var logger = A.Fake<ILoggerFactory>();

            var cut = new GameCleaner(gameRepo, cleanedGameRepo, playerRepo, logger);

            A.CallTo(() => cleanedGameRepo.GetSeasonOfCleanedGames(A<int>.Ignored)).Returns(new List<DbCleanedGame>());
            A.CallTo(() => gameRepo.GetSeasonGames(A<int>.Ignored)).Returns(new List<Game>());

            await cut.CleanGamesInSeasons(mockYearRange);

            A.CallTo(() => cleanedGameRepo.Commit()).MustNotHaveHappened();
        }
        [TestMethod]
        public async Task CallToCleanGamesInSeasons_WithUncleanedGames_ShouldCallCommitWithGames()
        {
            var gameRepo = A.Fake<IGameRepository>();
            var cleanedGameRepo = A.Fake<ICleanedGameRepository>();
            var playerRepo = A.Fake<IPlayerRepository>();
            var logger = A.Fake<ILoggerFactory>();

            var cut = new GameCleaner(gameRepo, cleanedGameRepo, playerRepo, logger);

            A.CallTo(() => cleanedGameRepo.GetSeasonOfCleanedGames(A<int>.Ignored)).Returns(new List<DbCleanedGame>());
            A.CallTo(() => gameRepo.GetSeasonGames(A<int>.Ignored)).Returns(GetUncleanGames());

            await cut.CleanGamesInSeasons(mockYearRange);

            A.CallTo(() => cleanedGameRepo.Commit()).MustHaveHappened();
        }

        private Task<IEnumerable<Game>> GetUncleanGames()
        {
            IEnumerable<Game> games = new List<Game>()
            {
                new Game()
                {
                    id = 8,
                    homeTeamId = 7,
                    awayTeamId = 8,
                }
            };

            return Task.FromResult(games);
        }
    }
}
