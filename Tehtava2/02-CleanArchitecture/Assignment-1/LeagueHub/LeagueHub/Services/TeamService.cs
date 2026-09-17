using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;


namespace LeagueHub.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamMemoryRepository _teams;

        public TeamService(ITeamMemoryRepository teams)
        {
            _teams = teams;
        }

        public List<Team> GetAllTeams()
        {
            return _teams.GetAllTeams();
        }
        public Team? GetById(int id)
        {

            if (id <= 0)
            {
                throw new TeamExpection("Id must be greater than 0");
            }
            else
            {
                Team team = _teams.GetTeamById(id);
                return team;
            }
        }

        public Team AddTeam(Team team)
        {
            var existingTeam = _teams.GetTeamById(team.Id);

            var allTeams = _teams.GetAllTeams();

            if (existingTeam != null)
            {
                throw new TeamExpection("Team with the same id already exists");
            }
            else if (allTeams.Any(t => t.Name == team.Name))
            {
                throw new TeamExpection("Team with the same name already exists");
            }
            else if (team == null)
            {
                throw new TeamExpection("Team cannot be null");
            }
            else if (string.IsNullOrEmpty(team.Name))
            {
                throw new TeamExpection("Team name cannot be null or empty");
            }
            else if (string.IsNullOrEmpty(team.City))
            {
                throw new TeamExpection("Team city cannot be null or empty");
            }
            else if (team.MaxPlayersOnField <= 0)
            {
                throw new TeamExpection("Max players on field must be greater than 0");
            }
            else
            {
                return _teams.AddTeam(team);
            }
        }
        public void RemoveTeam(Team team)
        {
            var existingTeams = _teams.GetAllTeams();

            if (existingTeams.Any(t => t.Id == team.Id))
            {
                _teams.Remove(team);
            }
            else
            {
                throw new TeamExpection("Team does not exist");
            }
        }
    }
}
