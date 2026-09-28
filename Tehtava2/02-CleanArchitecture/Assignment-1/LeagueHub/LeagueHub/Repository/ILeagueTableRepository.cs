using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public interface ILeagueTableRepository
    {
        List<LeagueTable> GetAllEntries();

        LeagueTable? GetEntryById(int id);

        LeagueTable AddEntry(LeagueTable entry);

        void RemoveEntry(LeagueTable entry);
    }
}
