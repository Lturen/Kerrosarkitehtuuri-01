using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public class InMemoryMatchScoreRepository : IMatchScoreRepository
    {
        private readonly List<MatchScore> _matchScores = new()
        {
            new MatchScore { Id = 1, MatchId = 1, HomeTeamScore = 2, AwayTeamScore = 1 }
        };

        public List<MatchScore> GetAllMatchScores()
        {
            return _matchScores;
        }

        public MatchScore? GetMatchScoreById(int id)
        {
            return _matchScores.FirstOrDefault(s => s.Id == id);
        }

        public MatchScore AddMatchScore(MatchScore matchScore)
        {
            _matchScores.Add(matchScore);
            return matchScore;
        }

        public void RemoveMatchScore(MatchScore matchScore)
        {
            _matchScores.Remove(matchScore);
        }
    }
}
