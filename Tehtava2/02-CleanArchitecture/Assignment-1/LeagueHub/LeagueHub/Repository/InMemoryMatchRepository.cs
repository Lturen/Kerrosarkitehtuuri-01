using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public class InMemoryMatchRepository : IMatchRepository
    {
        private readonly List<Match> _matches = new()
        {
            new Match
            {
                Id = 1,
                DateTime = new DateTime(2026, 10, 4, 18, 0, 0),
                HomeTeam = new Team { Id = 1, Name = "Kouvolanpallo", City = "Kouvola", MaxPlayersOnField = 11 },
                AwayTeam = new Team { Id = 2, Name = "Jukurit", City = "Mikkeli", MaxPlayersOnField = 11 },
                Location = "Kouvolan stadion"
            }
        };

        public List<Match> GetAllMatches()
        {
            return _matches;
        }

        public Match? GetMatchById(int id)
        {
            return _matches.FirstOrDefault(m => m.Id == id);
        }

        public Match AddMatch(Match match)
        {
            _matches.Add(match);
            return match;
        }

        public void RemoveMatch(Match match)
        {
            _matches.Remove(match);
        }
    }
}
