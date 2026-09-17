using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Tests.Fakes;

public class FakeTeamRepository : ITeamRepository
{
    public List<Team> Teams { get; } = new();
    private int _nextId = 1;

    public Task<List<Team>> GetAllAsync() => Task.FromResult(Teams.ToList());

    public Task<Team?> GetByIdAsync(int id) =>
        Task.FromResult(Teams.FirstOrDefault(t => t.Id == id));

    public Task<Team> AddAsync(Team team)
    {
        team.AssignId(_nextId++);
        Teams.Add(team);
        return Task.FromResult(team);
    }
}
