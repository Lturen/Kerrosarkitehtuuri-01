using LeagueHub.Application.Exceptions;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Players;

// LH-2: rosterin raja on entiteetissä (Team.EnsureCanAddPlayer).
// Pelinumeron uniikkius vaatii muut pelaajat → use casen sääntö.
public class AddPlayerToTeamUseCase
{
    private readonly ITeamRepository _teams;
    private readonly IPlayerRepository _players;

    public AddPlayerToTeamUseCase(ITeamRepository teams, IPlayerRepository players)
    {
        _teams = teams;
        _players = players;
    }

    public async Task<Player> ExecuteAsync(int teamId, string name, int number)
    {
        Team? team = await _teams.GetByIdAsync(teamId);

        if (team is null)
        {
            throw new NotFoundException("Joukkuetta ei löydy.");
        }

        List<Player> roster = await _players.GetByTeamIdAsync(teamId);

        team.EnsureCanAddPlayer(roster.Count);

        if (roster.Any(p => p.Number == number))
        {
            throw new DomainException("Pelinumero on jo käytössä joukkueessa.");
        }

        Player player = Player.Create(teamId, name, number);
        return await _players.AddAsync(player);
    }
}
