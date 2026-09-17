using LeagueHub.Models;

namespace LeagueHub.Services
{
    public interface ITeamService
    {
        List<Team> GetAllTeams();

        Team? GetById(int id);

        Team AddTeam(Team team);

        void RemoveTeam(Team team);



    }
}
