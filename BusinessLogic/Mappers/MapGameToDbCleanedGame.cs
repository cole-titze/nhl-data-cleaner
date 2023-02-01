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
        /// <param name="seasonGames">Dictionary of team ids and Team games</param>
        /// <returns>A cleaned game</returns>
        public static DbCleanedGame Map(Game game, SeasonGames seasonGames)
		{
            var homeTeamGames = seasonGames.GamesMap[game.homeTeamId];
            var awayTeamGames = seasonGames.GamesMap[game.awayTeamId];
            // Lists of team games for current season
            var homeTeamSeasonGames = homeTeamGames.CurrentSeasonGames.GetGamesBeforeDate(game.gameDate);
            var awayTeamSeasonGames = awayTeamGames.CurrentSeasonGames.GetGamesBeforeDate(game.gameDate);
            // List of recently played team games
            var homeTeamRecentGames = homeTeamGames.Games.GetGamesBeforeDate(game.gameDate).Take(RECENT_GAMES);
            var awayTeamRecentGames = awayTeamGames.Games.GetGamesBeforeDate(game.gameDate).Take(RECENT_GAMES);
            // List of team games played that match current home/away position
            var homeTeamHomeGames = homeTeamGames.HomeGames.GetGamesBeforeDate(game.gameDate);
            var awayTeamAwayGames = awayTeamGames.AwayGames.GetGamesBeforeDate(game.gameDate);
            // List of recent team games played that match current home/away position
            var homeTeamRecentHomeGames = homeTeamGames.HomeGames.GetGamesBeforeDate(game.gameDate).Take(RECENT_GAMES);
            var awayTeamRecentAwayGames = awayTeamGames.AwayGames.GetGamesBeforeDate(game.gameDate).Take(RECENT_GAMES);

            var cleanedGame = new DbCleanedGame()
            {
                gameId = game.id,

                homeWinRatio = GetWinRatioOfGames(homeTeamSeasonGames, game.homeTeamId),
                homeRecentWinRatio = GetWinRatioOfGames(homeTeamRecentGames, game.homeTeamId),
                homeGoalsAvg = GetGoalsAvgOfGames(homeTeamSeasonGames, game.homeTeamId),
                homeRecentGoalsAvg = GetGoalsAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeConcededGoalsAvg = GetConcededGoalsAvgOfGames(homeTeamSeasonGames, game.homeTeamId),
                homeRecentConcededGoalsAvg = GetConcededGoalsAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentSogAvg = GetSogAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentBlockedShotsAvg = GetBlockedShotsAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentPpgAvg = GetPpgAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentHitsAvg = GetHitsAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentPimAvg = GetPimAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentTakeawaysAvg = GetTakeawaysAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeRecentGiveawaysAvg = GetGiveawaysAvgOfGames(homeTeamRecentGames, game.homeTeamId),
                homeConcededGoalsAvgAtHome = GetConcededGoalsAvgOfGames(homeTeamHomeGames, game.homeTeamId),
                homeRecentConcededGoalsAvgAtHome = GetConcededGoalsAvgOfGames(homeTeamHomeGames.Take(RECENT_GAMES), game.homeTeamId),
                homeGoalsAvgAtHome = GetGoalsAvgOfGames(homeTeamHomeGames, game.homeTeamId),
                homeRecentGoalsAvgAtHome = GetGoalsAvgOfGames(homeTeamRecentHomeGames, game.homeTeamId),
                homeHoursSinceLastGame = game.GetHoursBetweenGames(homeTeamSeasonGames.FirstOrDefault()),
                homeRosterDefenseValue = game.teamRosters.homeDefensePlayers.GetRosterPlayersValue(),
                homeRosterOffenseValue = game.teamRosters.homeOffensePlayers.GetRosterPlayersValue(),
                homeRosterGoalieValue = game.teamRosters.homeGoalies.GetRosterPlayersValue(),


                awayWinRatio = GetWinRatioOfGames(awayTeamSeasonGames, game.awayTeamId),
                awayRecentWinRatio = GetWinRatioOfGames(awayTeamRecentGames, game.awayTeamId),
                awayGoalsAvg = GetGoalsAvgOfGames(awayTeamSeasonGames, game.awayTeamId),
                awayRecentGoalsAvg = GetGoalsAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayConcededGoalsAvg = GetConcededGoalsAvgOfGames(awayTeamSeasonGames, game.awayTeamId),
                awayRecentConcededGoalsAvg = GetConcededGoalsAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentSogAvg = GetSogAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentBlockedShotsAvg = GetBlockedShotsAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentPpgAvg = GetPpgAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentHitsAvg = GetHitsAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentPimAvg = GetPimAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentTakeawaysAvg = GetTakeawaysAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayRecentGiveawaysAvg = GetGiveawaysAvgOfGames(awayTeamRecentGames, game.awayTeamId),
                awayConcededGoalsAvgAtAway = GetConcededGoalsAvgOfGames(awayTeamAwayGames, game.awayTeamId),
                awayRecentConcededGoalsAvgAtAway = GetConcededGoalsAvgOfGames(awayTeamRecentAwayGames, game.awayTeamId),
                awayGoalsAvgAtAway = GetGoalsAvgOfGames(awayTeamAwayGames, game.awayTeamId),
                awayRecentGoalsAvgAtAway = GetGoalsAvgOfGames(awayTeamRecentAwayGames, game.awayTeamId),
                awayHoursSinceLastGame = game.GetHoursBetweenGames(awayTeamSeasonGames.FirstOrDefault()),
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
        /// <returns>The ratio of wins</returns>
        public static double GetWinRatioOfGames(IEnumerable<Game> teamSeasonGames, int teamId)
        {
            double winRatio = 0;
            int count = 0;
            foreach (var game in teamSeasonGames)
            {
                if (game.IsWin(teamId))
                    winRatio++;
                count++;
            }
            if (count > 0)
                winRatio = winRatio / count;
            return winRatio;
        }
        /// <summary>
        /// Gets the goal average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of games sorted by date (recent first)</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Goals average</returns>
        public static double GetGoalsAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double goalsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the conceded goal average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">list of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Conceded goals average</returns>
        public static double GetConcededGoalsAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double goalsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the shots on goal average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">list of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Shots on goal average</returns>
        public static double GetSogAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double sogAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the blocked shots average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of team games</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Blocked shots average</returns>
        public static double GetBlockedShotsAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double blockedSogAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the power play goal average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Power play goals average</returns>
        public static double GetPpgAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double ppgAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the hits average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Hits average</returns>
        public static double GetHitsAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double hitsAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the penalty minute average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Penalty minute average</returns>
        public static double GetPimAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double pimAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the Takeaway average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of games played</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Takeaway average</returns>
        public static double GetTakeawaysAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double takeawayAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
        /// <summary>
        /// Gets the giveaway average of the games, for the given team
        /// </summary>
        /// <param name="teamGames">List of team games</param>
        /// <param name="teamId">Team id</param>
        /// <returns>Giveaway average</returns>
        public static double GetGiveawaysAvgOfGames(IEnumerable<Game> teamGames, int teamId)
        {
            double giveawayAvg = 0;
            int count = 0;
            foreach (var game in teamGames)
            {
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
