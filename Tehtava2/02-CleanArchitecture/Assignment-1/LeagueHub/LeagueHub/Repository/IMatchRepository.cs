using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public interface IMatchRepository
    {
        List<Match> GetAllMatches();

        Match? GetMatchById(int id);

        Match AddMatch(Match match);

        void RemoveMatch(Match match);
    }
}
