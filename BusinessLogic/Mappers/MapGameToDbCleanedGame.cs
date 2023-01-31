using Entities.DbModels;
using Entities.Models;

namespace BusinessLogic.Mappers
{
	public static class MapGameToDbCleanedGame
	{
        private const int RECENT_GAMES = 5;
        private const int EARLY_SEASON_GAME_COUNT = 15;
        /// <summary>
        /// Converts a raw game into a cleaned, ML ready, game. Uses previous games for certain features.
        /// </summary>
        /// <param name="game">Game to clean</param>
        /// <param name="seasonGames">Games from the current season</param>
        /// <param name="lastSeasonGames">Games from the previous season</param>
        /// <returns>A cleaned game</returns>
        public static DbCleanedGame Map(Game game, SeasonGames teamGames)
		{
            var homeTeamGames = teamGames.GamesMap[game.homeTeamId].Games.GetGamesBeforeDate(game.gameDate);
            var awayTeamGames = teamGames.GamesMap[game.awayTeamId].Games.GetGamesBeforeDate(game.gameDate);

            var homeTeamHomeGames = teamGames.GamesMap[game.homeTeamId].HomeGames.GetGamesBeforeDate(game.gameDate);
            var awayTeamAwayGames = teamGames.GamesMap[game.awayTeamId].AwayGames.GetGamesBeforeDate(game.gameDate);
            
            var cleanedGame = new DbCleanedGame()
            {
                gameId = game.id,

                homeWinRatio = GetWinRatioOfRecentGames(homeTeamGames, game.homeTeamId, homeTeamGames.Count()),
                homeRecentWinRatio = GetWinRatioOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeGoalsAvg = GetGoalsAvgOfRecentGames(homeTeamGames, game.homeTeamId, homeTeamGames.Count()),
                homeRecentGoalsAvg = GetGoalsAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(homeTeamGames, game.homeTeamId, homeTeamGames.Count()),
                homeRecentConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentSogAvg = GetSogAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentBlockedShotsAvg = GetBlockedShotsAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentPpgAvg = GetPpgAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentHitsAvg = GetHitsAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentPimAvg = GetPimAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentTakeawaysAvg = GetTakeawaysAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeRecentGiveawaysAvg = GetGiveawaysAvgOfRecentGames(homeTeamGames, game.homeTeamId, RECENT_GAMES),
                homeConcededGoalsAvgAtHome = GetConcededGoalsAvgOfRecentGames(homeTeamHomeGames, game.homeTeamId, homeTeamGames.Count()),
                homeRecentConcededGoalsAvgAtHome = GetConcededGoalsAvgOfRecentGames(homeTeamHomeGames, game.homeTeamId, RECENT_GAMES),
                homeGoalsAvgAtHome = GetGoalsAvgOfRecentGames(homeTeamHomeGames, game.homeTeamId, homeTeamGames.Count()),
                homeRecentGoalsAvgAtHome = GetGoalsAvgOfRecentGames(homeTeamHomeGames, game.homeTeamId, RECENT_GAMES),
                homeHoursSinceLastGame = game.GetHoursBetweenGames(homeTeamGames.FirstOrDefault()),
                homeRosterDefenseValue = game.teamRosters.homeDefensePlayers.GetRosterPlayersValue(),
                homeRosterOffenseValue = game.teamRosters.homeOffensePlayers.GetRosterPlayersValue(),
                homeRosterGoalieValue = game.teamRosters.homeGoalies.GetRosterPlayersValue(),


                awayWinRatio = GetWinRatioOfRecentGames(awayTeamGames, game.awayTeamId, awayTeamGames.Count()),
                awayRecentWinRatio = GetWinRatioOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayGoalsAvg = GetGoalsAvgOfRecentGames(awayTeamGames, game.awayTeamId, awayTeamGames.Count()),
                awayRecentGoalsAvg = GetGoalsAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(awayTeamGames, game.awayTeamId, awayTeamGames.Count()),
                awayRecentConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentSogAvg = GetSogAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentBlockedShotsAvg = GetBlockedShotsAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentPpgAvg = GetPpgAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentHitsAvg = GetHitsAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentPimAvg = GetPimAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentTakeawaysAvg = GetTakeawaysAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayRecentGiveawaysAvg = GetGiveawaysAvgOfRecentGames(awayTeamGames, game.awayTeamId, RECENT_GAMES),
                awayConcededGoalsAvgAtAway = GetConcededGoalsAvgOfRecentGames(awayTeamAwayGames, game.awayTeamId, awayTeamGames.Count()),
                awayRecentConcededGoalsAvgAtAway = GetConcededGoalsAvgOfRecentGames(awayTeamAwayGames, game.awayTeamId, RECENT_GAMES),
                awayGoalsAvgAtAway = GetGoalsAvgOfRecentGames(awayTeamAwayGames, game.awayTeamId, awayTeamGames.Count()),
                awayRecentGoalsAvgAtAway = GetGoalsAvgOfRecentGames(awayTeamAwayGames, game.awayTeamId, RECENT_GAMES),
                awayHoursSinceLastGame = game.GetHoursBetweenGames(awayTeamGames.FirstOrDefault()),
                awayRosterDefenseValue = game.teamRosters.awayDefensePlayers.GetRosterPlayersValue(),
                awayRosterOffenseValue = game.teamRosters.awayOffensePlayers.GetRosterPlayersValue(),
                awayRosterGoalieValue = game.teamRosters.awayGoalies.GetRosterPlayersValue(),
            };
            return cleanedGame;
        }
        /// <summary>
        /// Gets the win ratio of the last games specified by numberOfGames
        /// </summary>
        /// <param name="teamSeasonGames">Games a team has played, sorted by recency</param>
        /// <param name="teamId">Team to get ratio of</param>
        /// <param name="numberOfGames">How many games to use</param>
        /// <returns>The ratio of wins</returns>
        public static double GetWinRatioOfRecentGames(IEnumerable<Game> teamSeasonGames, int teamId, int numberOfGames)
        {
            double winRatio = 0;
            int count = 0;
            foreach (var game in teamSeasonGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.IsWin(teamId))
                    winRatio++;
                count++;
            }
            if (count > 0)
                winRatio = winRatio / count;
            return winRatio;
        }

        public static double GetGoalsAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double goalsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    goalsAvg += game.homeGoals;
                else if (game.awayTeamId == teamId)
                    goalsAvg += game.awayGoals;

                count++;
            }
            if (count > 0)
                goalsAvg = goalsAvg / count;
            return goalsAvg;
        }
        public static double GetConcededGoalsAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double goalsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.awayTeamId == teamId)
                    goalsAvg += game.homeGoals;
                else if (game.homeTeamId == teamId)
                    goalsAvg += game.awayGoals;

                count++;
            }
            if (count > 0)
                goalsAvg = goalsAvg / count;
            return goalsAvg;
        }

        public static double GetSogAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double sogAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    sogAvg += game.homeSOG;
                else if (game.awayTeamId == teamId)
                    sogAvg += game.awaySOG;

                count++;
            }
            if (count > 0)
                sogAvg = sogAvg / count;
            return sogAvg;
        }

        public static double GetBlockedShotsAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double blockedSogAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    blockedSogAvg += game.homeBlockedShots;
                else if (game.awayTeamId == teamId)
                    blockedSogAvg += game.awayBlockedShots;

                count++;
            }
            if (count > 0)
                blockedSogAvg = blockedSogAvg / count;
            return blockedSogAvg;
        }
        public static double GetPpgAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double ppgAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    ppgAvg += game.homePPG;
                else if (game.awayTeamId == teamId)
                    ppgAvg += game.awayPPG;

                count++;
            }
            if (count > 0)
                ppgAvg = ppgAvg / count;
            return ppgAvg;
        }
        public static double GetHitsAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double hitsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    hitsAvg += game.homeHits;
                else if (game.awayTeamId == teamId)
                    hitsAvg += game.awayHits;

                count++;
            }
            if (count > 0)
                hitsAvg = hitsAvg / count;
            return hitsAvg;
        }
        public static double GetPimAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double pimAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    pimAvg += game.homePIM;
                else if (game.awayTeamId == teamId)
                    pimAvg += game.awayPIM;

                count++;
            }
            if (count > 0)
                pimAvg = pimAvg / count;
            return pimAvg;
        }
        public static double GetTakeawaysAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double takeawayAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    takeawayAvg += game.homeTakeaways;
                else if (game.awayTeamId == teamId)
                    takeawayAvg += game.awayTakeaways;

                count++;
            }
            if (count > 0)
                takeawayAvg = takeawayAvg / count;
            return takeawayAvg;
        }

        public static double GetGiveawaysAvgOfRecentGames(IEnumerable<Game> teamGames, int teamId, int numberOfGames)
        {
            double giveawayAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
                if (count == numberOfGames)
                    break;
                if (game.homeTeamId == teamId)
                    giveawayAvg += game.homeGiveaways;
                else if (game.awayTeamId == teamId)
                    giveawayAvg += game.awayGiveaways;

                count++;
            }
            if (count > 0)
                giveawayAvg = giveawayAvg / count;
            return giveawayAvg;
        }
    }
}
