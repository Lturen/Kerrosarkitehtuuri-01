using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public interface ITeamMemoryRepository
    {
        List<Team> GetAllTeams();

        Team? GetTeamById(int id);

        Team? AddTeam(Team team);

        void Remove(Team team);
    }
}
