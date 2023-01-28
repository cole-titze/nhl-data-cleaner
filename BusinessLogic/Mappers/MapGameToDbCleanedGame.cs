using Entities.DbModels;
using Entities.Models;

namespace BusinessLogic.Mappers
{
	public static class MapGameToDbCleanedGame
	{
        private const int RECENT_GAMES = 5;
        private const int EARLY_SEASON_GAME_COUNT = 15;

        public static DbCleanedGame Map(Game game, List<Game> seasonGames, List<Game> lastSeasonGames)
		{
            var homeGames = GetTeamGames(seasonGames, game.homeTeamId, game.gameDate);
            var awayGames = GetTeamGames(seasonGames, game.awayTeamId, game.gameDate);
            List<Game> lastSeasonHomeGames;
            List<Game> lastSeasonAwayGames;
            var homeHoursBetweenGames = GetHoursBetweenLastTwoGames(homeGames);
            var awayHoursBetweenGames = GetHoursBetweenLastTwoGames(awayGames);

            if (homeGames.Count() < EARLY_SEASON_GAME_COUNT)
            {
                lastSeasonHomeGames = GetTeamGames(lastSeasonGames, game.homeTeamId, game.gameDate);
                homeGames = lastSeasonHomeGames.Concat(homeGames).ToList();
            }
            if (awayGames.Count() < EARLY_SEASON_GAME_COUNT)
            {
                lastSeasonAwayGames = GetTeamGames(lastSeasonGames, game.awayTeamId, game.gameDate);
                awayGames = lastSeasonAwayGames.Concat(awayGames).ToList();
            }

            var cleanedGame = new DbCleanedGame()
            {
                gameId = game.id,

                homeWinRatio = GetWinRatioOfRecentGames(homeGames, game.homeTeamId, homeGames.Count()),
                homeRecentWinRatio = GetWinRatioOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeGoalsAvg = GetGoalsAvgOfRecentGames(homeGames, game.homeTeamId, homeGames.Count()),
                homeRecentGoalsAvg = GetGoalsAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(homeGames, game.homeTeamId, homeGames.Count()),
                homeRecentConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentSogAvg = GetSogAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentBlockedShotsAvg = GetBlockedShotsAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentPpgAvg = GetPpgAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentHitsAvg = GetHitsAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentPimAvg = GetPimAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentTakeawaysAvg = GetTakeawaysAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeRecentGiveawaysAvg = GetGiveawaysAvgOfRecentGames(homeGames, game.homeTeamId, RECENT_GAMES),
                homeConcededGoalsAvgAtHome = GetConcededGoalsAvgOfRecentGamesAtHome(homeGames, game.homeTeamId, homeGames.Count()),
                homeRecentConcededGoalsAvgAtHome = GetConcededGoalsAvgOfRecentGamesAtHome(homeGames, game.homeTeamId, RECENT_GAMES),
                homeGoalsAvgAtHome = GetGoalsAvgOfRecentGamesAtHome(homeGames, game.homeTeamId, homeGames.Count()),
                homeRecentGoalsAvgAtHome = GetGoalsAvgOfRecentGamesAtHome(homeGames, game.homeTeamId, RECENT_GAMES),
                homeHoursSinceLastGame = homeHoursBetweenGames,
                homeRosterDefenseValue = GetRosterPlayersValue(game.teamRosters.homeDefensePlayers),
                homeRosterOffenseValue = GetRosterPlayersValue(game.teamRosters.homeOffensePlayers),
                homeRosterGoalieValue = GetRosterPlayersValue(game.teamRosters.homeGoalies),


                awayWinRatio = GetWinRatioOfRecentGames(awayGames, game.awayTeamId, awayGames.Count()),
                awayRecentWinRatio = GetWinRatioOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayGoalsAvg = GetGoalsAvgOfRecentGames(awayGames, game.awayTeamId, awayGames.Count()),
                awayRecentGoalsAvg = GetGoalsAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(awayGames, game.awayTeamId, awayGames.Count()),
                awayRecentConcededGoalsAvg = GetConcededGoalsAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentSogAvg = GetSogAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentBlockedShotsAvg = GetBlockedShotsAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentPpgAvg = GetPpgAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentHitsAvg = GetHitsAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentPimAvg = GetPimAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentTakeawaysAvg = GetTakeawaysAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayRecentGiveawaysAvg = GetGiveawaysAvgOfRecentGames(awayGames, game.awayTeamId, RECENT_GAMES),
                awayConcededGoalsAvgAtAway = GetConcededGoalsAvgOfRecentGamesAtAway(awayGames, game.awayTeamId, awayGames.Count()),
                awayRecentConcededGoalsAvgAtAway = GetConcededGoalsAvgOfRecentGamesAtAway(awayGames, game.awayTeamId, RECENT_GAMES),
                awayGoalsAvgAtAway = GetGoalsAvgOfRecentGamesAtAway(awayGames, game.awayTeamId, awayGames.Count()),
                awayRecentGoalsAvgAtAway = GetGoalsAvgOfRecentGamesAtAway(awayGames, game.awayTeamId, RECENT_GAMES),
                awayHoursSinceLastGame = awayHoursBetweenGames,
                awayRosterDefenseValue = GetRosterPlayersValue(game.teamRosters.awayDefensePlayers),
                awayRosterOffenseValue = GetRosterPlayersValue(game.teamRosters.awayOffensePlayers),
                awayRosterGoalieValue = GetRosterPlayersValue(game.teamRosters.awayGoalies),
            };
            return cleanedGame;
        }

        private static double GetRosterPlayersValue(List<DbPlayer> players)
        {
            double skillScore = 0;
            foreach(var player in players)
            {
                skillScore += player.value;
            }

            return skillScore;
        }

        // If no game has been played set default as 4 days of rest (season hasn't started)
        public static readonly int DEFAULT_HOURS = 100;
        public static double GetWinRatioOfRecentGames(List<Game> teamSeasonGames, int teamId, int numberOfGames)
        {
            double winRatio = 0;
            int count = 0;
            foreach (var game in teamSeasonGames)
            {
                if (count == numberOfGames)
                    break;
                if (isWin(game, teamId))
                    winRatio++;
                count++;
            }
            if (count > 0)
                winRatio = winRatio / count;
            return winRatio;
        }

        private static bool isWin(Game game, int teamId)
        {
            if (game.homeTeamId == teamId && game.winner == Winner.HOME) return true;
            if (game.awayTeamId == teamId && game.winner == Winner.AWAY) return true;
            return false;
        }

        public static double GetGoalsAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetConcededGoalsAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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

        public static double GetSogAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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

        public static double GetBlockedShotsAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetPpgAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetHitsAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetPimAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetTakeawaysAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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

        public static bool GetIsExcluded(List<Game> awayGames, List<Game> homeGames, int numberOfGamesToExclude)
        {
            if (awayGames.Count() < numberOfGamesToExclude || homeGames.Count() < numberOfGamesToExclude)
                return true;
            return false;
        }

        public static double GetGiveawaysAvgOfRecentGames(List<Game> teamGames, int teamId, int numberOfGames)
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
        public static double GetConcededGoalsAvgOfRecentGamesAtHome(List<Game> teamGames, int teamId, int numberOfGames)
        {
            teamGames = teamGames.Where(game => game.homeTeamId == teamId).ToList();
            return GetConcededGoalsAvgOfRecentGames(teamGames, teamId, numberOfGames);
        }
        public static double GetConcededGoalsAvgOfRecentGamesAtAway(List<Game> teamGames, int teamId, int numberOfGames)
        {
            teamGames = teamGames.Where(game => game.awayTeamId == teamId).ToList();
            return GetConcededGoalsAvgOfRecentGames(teamGames, teamId, numberOfGames);
        }
        public static double GetGoalsAvgOfRecentGamesAtHome(List<Game> teamGames, int teamId, int numberOfGames)
        {
            teamGames = teamGames.Where(game => game.homeTeamId == teamId).ToList();
            return GetGoalsAvgOfRecentGames(teamGames, teamId, numberOfGames);
        }
        public static double GetGoalsAvgOfRecentGamesAtAway(List<Game> teamGames, int teamId, int numberOfGames)
        {
            teamGames = teamGames.Where(game => game.awayTeamId == teamId).ToList();
            return GetGoalsAvgOfRecentGames(teamGames, teamId, numberOfGames);
        }
        // If only one game has been played return default rest, otherwise find how many hours
        // since last game
        public static double GetHoursBetweenLastTwoGames(List<Game> games)
        {
            if (games.Count() < 2)
                return DEFAULT_HOURS;
            var currentDate = games[0].gameDate;
            var lastDate = games[1].gameDate;

            var hourDifference = (currentDate - lastDate).TotalHours;

            return hourDifference;
        }
        private static List<Game> GetTeamGames(List<Game> seasonsGames, int teamId, DateTime currentGameDate)
        {
            // Get games that happened before current game and include the team
            return seasonsGames.Where(i => i.gameDate < currentGameDate)
                .Where(n => n.awayTeamId == teamId || n.homeTeamId == teamId).ToList();
        }
    }
}

