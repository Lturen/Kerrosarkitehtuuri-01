using LeagueHub.Domain.Entities;

namespace LeagueHub.Domain.Interfaces;

public interface IMatchRepository
{
    Task<List<Match>> GetAllAsync();
    Task<Match?> GetByIdAsync(int id);
    Task<Match> AddAsync(Match match);

    // Tarvitaan, koska RecordResult muuttaa olemassa olevan olion tilaa.
    Task UpdateAsync(Match match);
}
