using LeagueHub.Models;

namespace LeagueHub.Services
{
    public interface IMatchScoreService
    {
        List<MatchScore> GetAllMatchScores();

        MatchScore? GetMatchScoreById(int id);

        MatchScore AddMatchScore(MatchScore matchScore);

        void RemoveMatchScore(MatchScore matchScore);
    }
}
