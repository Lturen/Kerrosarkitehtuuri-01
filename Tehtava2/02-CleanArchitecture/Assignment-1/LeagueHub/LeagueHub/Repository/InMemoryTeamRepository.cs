using LeagueHub.Models;

namespace LeagueHub.Repository
{
    public class InMemoryTeamRepository : ITeamMemoryRepository
    {
        private readonly List<Team> _teams = new()
        {

        new Team { Id = 1, Name = "Kouvolanpallo", City = "Kouvola", MaxPlayersOnField = 11 },
        new Team { Id = 2, Name = "Jukurit", City = "Mikkeli", MaxPlayersOnField = 11 }
        };

        public List<Team> GetAllTeams()
        {
            return _teams;
        }

        public Team? GetTeamById(int id)
        {
            return _teams.FirstOrDefault(t => t.Id == id);
        }
        
        public Team? AddTeam(Team team)
        {
            return team;
        }

        public void Remove(Team team)
        {
            _teams.Remove(team);
        }
    }
}
