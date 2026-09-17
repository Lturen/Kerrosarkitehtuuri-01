using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Tests.Fakes;

public class FakeMatchRepository : IMatchRepository
{
    public List<Match> Matches { get; } = new();
    private int _nextId = 1;

    public Task<List<Match>> GetAllAsync() => Task.FromResult(Matches.ToList());

    public Task<Match?> GetByIdAsync(int id) =>
        Task.FromResult(Matches.FirstOrDefault(m => m.Id == id));

    public Task<Match> AddAsync(Match match)
    {
        match.AssignId(_nextId++);
        Matches.Add(match);
        return Task.FromResult(match);
    }

    // Olio on jo listassa ja tila muuttui entiteetissä — ei tehtävää.
    public Task UpdateAsync(Match match) => Task.CompletedTask;
}
