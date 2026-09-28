using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public class InMemoryLeagueTableRepository : ILeagueTableRepository
    {
        private readonly List<LeagueTable> _entries = new()
        {
            new LeagueTable { Id = 1, TeamId = 1, TeamName = "Kouvolanpallo", Points = 3, Wins = 1, Draws = 0, Losses = 0 },
            new LeagueTable { Id = 2, TeamId = 2, TeamName = "Jukurit", Points = 0, Wins = 0, Draws = 0, Losses = 1 }
        };

        public List<LeagueTable> GetAllEntries()
        {
            return _entries;
        }

        public LeagueTable? GetEntryById(int id)
        {
            return _entries.FirstOrDefault(e => e.Id == id);
        }

        public LeagueTable AddEntry(LeagueTable entry)
        {
            _entries.Add(entry);
            return entry;
        }

        public void RemoveEntry(LeagueTable entry)
        {
            _entries.Remove(entry);
        }
    }
}
