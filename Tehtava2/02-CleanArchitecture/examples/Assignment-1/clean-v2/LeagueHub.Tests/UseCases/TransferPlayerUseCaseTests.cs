using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Players;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Tests.Fakes;

namespace LeagueHub.Tests.UseCases;

public class TransferPlayerUseCaseTests
{
    private readonly FakeTeamRepository _teams = new();
    private readonly FakePlayerRepository _players = new();
    private readonly TransferPlayerUseCase _useCase;

    public TransferPlayerUseCaseTests()
    {
        _useCase = new TransferPlayerUseCase(_teams, _players);
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenPlayerIsMissing()
    {
        Team target = await _teams.AddAsync(Team.Create("B", "Y", 10));

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.ExecuteAsync(playerId: 999, target.Id));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenTargetTeamIsMissing()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));
        Player player = await _players.AddAsync(Player.Create(home.Id, "Pekka", 10));

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.ExecuteAsync(player.Id, targetTeamId: 999));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenNumberIsTakenInTarget()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));
        Team target = await _teams.AddAsync(Team.Create("B", "Y", 10));
        Player player = await _players.AddAsync(Player.Create(home.Id, "Pekka", 10));
        await _players.AddAsync(Player.Create(target.Id, "Maija", 10));

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(player.Id, target.Id));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenTargetRosterIsFull()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));
        Team target = await _teams.AddAsync(Team.Create("B", "Y", 1));
        Player player = await _players.AddAsync(Player.Create(home.Id, "Pekka", 10));
        await _players.AddAsync(Player.Create(target.Id, "Maija", 7));

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(player.Id, target.Id));
    }

    [Fact]
    public async Task ExecuteAsync_TransfersPlayer_WhenValid()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));
        Team target = await _teams.AddAsync(Team.Create("B", "Y", 10));
        Player player = await _players.AddAsync(Player.Create(home.Id, "Pekka", 10));

        Player updated = await _useCase.ExecuteAsync(player.Id, target.Id);

        Assert.Equal(target.Id, updated.TeamId);
    }
}
