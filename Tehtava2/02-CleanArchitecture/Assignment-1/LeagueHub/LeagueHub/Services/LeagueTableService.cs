using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;

namespace LeagueHub.Services
{
    public class LeagueTableService : ILeagueTableService
    {
        private readonly ILeagueTableRepository _entries;

        public LeagueTableService(ILeagueTableRepository entries)
        {
            _entries = entries;
        }

        public List<LeagueTable> GetAllEntries()
        {
            return _entries.GetAllEntries();
        }

        public LeagueTable? GetEntryById(int id)
        {
            if (id <= 0)
            {
                throw new LeagueTableExpection("Id must be greater than 0", 400);
            }

            return _entries.GetEntryById(id);
        }

        public LeagueTable AddEntry(LeagueTable entry)
        {
            if (entry == null)
            {
                throw new LeagueTableExpection("League table entry cannot be null", 400);
            }
            else if (entry.TeamId <= 0)
            {
                throw new LeagueTableExpection("Team id must be greater than 0", 400);
            }
            else if (string.IsNullOrEmpty(entry.TeamName))
            {
                throw new LeagueTableExpection("Team name cannot be null or empty", 400);
            }
            else if (entry.Points < 0 || entry.Wins < 0 || entry.Draws < 0 || entry.Losses < 0)
            {
                throw new LeagueTableExpection("Points, wins, draws and losses cannot be negative", 400);
            }
            else if (_entries.GetEntryById(entry.Id) != null)
            {
                throw new LeagueTableExpection("League table entry with the same id already exists", 409);
            }
            else if (_entries.GetAllEntries().Any(e => e.TeamId == entry.TeamId))
            {
                throw new LeagueTableExpection("League table entry for this team already exists", 409);
            }
            else
            {
                return _entries.AddEntry(entry);
            }
        }

        public void RemoveEntry(LeagueTable entry)
        {
            var existingEntries = _entries.GetAllEntries();

            if (existingEntries.Any(e => e.Id == entry.Id))
            {
                _entries.RemoveEntry(entry);
            }
            else
            {
                throw new LeagueTableExpection("League table entry does not exist", 404);
            }
        }
    }
}
