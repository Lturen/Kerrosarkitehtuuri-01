using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;

namespace LeagueHub.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matches;

        public MatchService(IMatchRepository matches)
        {
            _matches = matches;
        }

        public List<Match> GetAllMatches()
        {
            return _matches.GetAllMatches();
        }

        public Match? GetMatchById(int id)
        {
            if (id <= 0)
            {
                throw new MatchExpection("Id must be greater than 0", 400);
            }

            return _matches.GetMatchById(id);
        }

        public Match AddMatch(Match match)
        {
            if (match == null)
            {
                throw new MatchExpection("Match cannot be null", 400);
            }
            else if (match.HomeTeam == null || match.AwayTeam == null)
            {
                throw new MatchExpection("Match must have a home team and an away team", 400);
            }
            else if (match.HomeTeam.Id == match.AwayTeam.Id)
            {
                throw new MatchExpection("Home team and away team cannot be the same team", 400);
            }
            else if (string.IsNullOrEmpty(match.Location))
            {
                throw new MatchExpection("Match location cannot be null or empty", 400);
            }
            else if (_matches.GetMatchById(match.Id) != null)
            {
                throw new MatchExpection("Match with the same id already exists", 409);
            }
            else
            {
                return _matches.AddMatch(match);
            }
        }

        public void RemoveMatch(Match match)
        {
            var existingMatches = _matches.GetAllMatches();

            if (existingMatches.Any(m => m.Id == match.Id))
            {
                _matches.RemoveMatch(match);
            }
            else
            {
                throw new MatchExpection("Match does not exist", 404);
            }
        }
    }
}
