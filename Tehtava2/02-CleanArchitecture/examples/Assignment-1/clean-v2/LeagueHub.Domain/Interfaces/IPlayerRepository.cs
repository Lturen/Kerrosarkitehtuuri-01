using LeagueHub.Domain.Entities;

namespace LeagueHub.Domain.Interfaces;

// Ei metodia IsNumberTaken — se olisi sääntö väärässä paikassa.
public interface IPlayerRepository
{
    Task<Player?> GetByIdAsync(int id);
    Task<List<Player>> GetByTeamIdAsync(int teamId);
    Task<Player> AddAsync(Player player);
    Task UpdateAsync(Player player);
}
