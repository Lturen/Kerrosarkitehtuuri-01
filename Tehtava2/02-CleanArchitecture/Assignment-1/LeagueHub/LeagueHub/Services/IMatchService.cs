using LeagueHub.Models;

namespace LeagueHub.Services
{
    public interface IMatchService
    {
        List<Match> GetAllMatches();

        Match? GetMatchById(int id);

        Match AddMatch(Match match);

        void RemoveMatch(Match match);
    }
}
