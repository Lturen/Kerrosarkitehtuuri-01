using LeagueHub.Application.Exceptions;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Players;

public class TransferPlayerUseCase
{
    private readonly ITeamRepository _teams;
    private readonly IPlayerRepository _players;

    public TransferPlayerUseCase(ITeamRepository teams, IPlayerRepository players)
    {
        _teams = teams;
        _players = players;
    }

    public async Task<Player> ExecuteAsync(int playerId, int targetTeamId)
    {
        Player? player = await _players.GetByIdAsync(playerId);

        if (player is null)
        {
            throw new NotFoundException("Pelaajaa ei löydy.");
        }

        Team? target = await _teams.GetByIdAsync(targetTeamId);

        if (target is null)
        {
            throw new NotFoundException("Kohdejoukkuetta ei löydy.");
        }

        // Roster haetaan ennen TransferTo:ta: muuten in-memory-fake näkisi
        // pelaajan jo kohteessa, ja uniikkius löytäisi hänen oman numeronsa.
        List<Player> roster = await _players.GetByTeamIdAsync(targetTeamId);

        player.TransferTo(targetTeamId);

        target.EnsureCanAddPlayer(roster.Count);

        if (roster.Any(p => p.Number == player.Number))
        {
            throw new DomainException("Pelinumero on jo käytössä joukkueessa.");
        }

        await _players.UpdateAsync(player);
        return player;
    }
}
