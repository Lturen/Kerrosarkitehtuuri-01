using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;

namespace LeagueHub.Services
{
    public class MatchScoreService : IMatchScoreService
    {
        private readonly IMatchScoreRepository _matchScores;
        private readonly IMatchService _matchService;

        public MatchScoreService(IMatchScoreRepository matchScores, IMatchService matchService)
        {
            _matchScores = matchScores;
            _matchService = matchService;
        }

        public List<MatchScore> GetAllMatchScores()
        {
            return _matchScores.GetAllMatchScores();
        }

        public MatchScore? GetMatchScoreById(int id)
        {
            if (id <= 0)
            {
                throw new MatchScoreExpection("Id must be greater than 0", 400);
            }

            return _matchScores.GetMatchScoreById(id);
        }

        public MatchScore AddMatchScore(MatchScore matchScore)
        {
            if (matchScore == null)
            {
                throw new MatchScoreExpection("Match score cannot be null", 400);
            }
            else if (matchScore.HomeTeamScore < 0 || matchScore.AwayTeamScore < 0)
            {
                throw new MatchScoreExpection("Scores cannot be negative", 400);
            }
            else if (_matchService.GetMatchById(matchScore.MatchId) == null)
            {
                throw new MatchScoreExpection($"Match with ID {matchScore.MatchId} not found", 404);
            }
            else if (_matchScores.GetMatchScoreById(matchScore.Id) != null)
            {
                throw new MatchScoreExpection("Match score with the same id already exists", 409);
            }
            else
            {
                return _matchScores.AddMatchScore(matchScore);
            }
        }

        public void RemoveMatchScore(MatchScore matchScore)
        {
            var existingScores = _matchScores.GetAllMatchScores();

            if (existingScores.Any(s => s.Id == matchScore.Id))
            {
                _matchScores.RemoveMatchScore(matchScore);
            }
            else
            {
                throw new MatchScoreExpection("Match score does not exist", 404);
            }
        }
    }
}
