using LeagueHub.Models;

namespace LeagueHub.Services
{
    public interface ILeagueTableService
    {
        List<LeagueTable> GetAllEntries();

        LeagueTable? GetEntryById(int id);

        LeagueTable AddEntry(LeagueTable entry);

        void RemoveEntry(LeagueTable entry);
    }
}
