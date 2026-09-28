using LeagueHub.Repository;
using LeagueHub.Models;
using LeagueHub.Expections;


namespace LeagueHub.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teams;

        public TeamService(ITeamRepository teams)
        {
            _teams = teams;
        }

        public List<Team> GetAllTeams()
        {
            return _teams.GetAllTeams();
        }

        public Team? GetTeamById(int id)
        {
            if (id <= 0)
            {
                throw new TeamExpection("Id must be greater than 0", 400);
            }

            return _teams.GetTeamById(id);
        }

        public Team AddTeam(Team team)
        {
            if (team == null)
            {
                throw new TeamExpection("Team cannot be null", 400);
            }
            else if (string.IsNullOrEmpty(team.Name))
            {
                throw new TeamExpection("Team name cannot be null or empty", 400);
            }
            else if (string.IsNullOrEmpty(team.City))
            {
                throw new TeamExpection("Team city cannot be null or empty", 400);
            }
            else if (team.MaxPlayersOnField <= 0)
            {
                throw new TeamExpection("Max players on field must be greater than 0", 400);
            }
            else if (_teams.GetTeamById(team.Id) != null)
            {
                throw new TeamExpection("Team with the same id already exists", 409);
            }
            else if (_teams.GetAllTeams().Any(t => t.Name == team.Name))
            {
                throw new TeamExpection("Team with the same name already exists", 409);
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
                throw new TeamExpection("Team does not exist", 404);
            }
        }
    }
}
