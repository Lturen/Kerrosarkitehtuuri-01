using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public interface IMatchScoreRepository
    {
        List<MatchScore> GetAllMatchScores();

        MatchScore? GetMatchScoreById(int id);

        MatchScore AddMatchScore(MatchScore matchScore);

        void RemoveMatchScore(MatchScore matchScore);
    }
}
