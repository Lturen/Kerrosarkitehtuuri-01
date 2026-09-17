using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Tests.Fakes;

public class FakePlayerRepository : IPlayerRepository
{
    public List<Player> Players { get; } = new();
    private int _nextId = 1;

    public Task<Player?> GetByIdAsync(int id) =>
        Task.FromResult(Players.FirstOrDefault(p => p.Id == id));

    public Task<List<Player>> GetByTeamIdAsync(int teamId) =>
        Task.FromResult(Players.Where(p => p.TeamId == teamId).ToList());

    public Task<Player> AddAsync(Player player)
    {
        player.AssignId(_nextId++);
        Players.Add(player);
        return Task.FromResult(player);
    }

    public Task UpdateAsync(Player player) => Task.CompletedTask;
}
