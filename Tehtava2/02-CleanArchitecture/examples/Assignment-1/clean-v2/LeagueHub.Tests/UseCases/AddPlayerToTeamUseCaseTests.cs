using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Players;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Tests.Fakes;

namespace LeagueHub.Tests.UseCases;

public class AddPlayerToTeamUseCaseTests
{
    private readonly FakeTeamRepository _teams = new();
    private readonly FakePlayerRepository _players = new();
    private readonly AddPlayerToTeamUseCase _useCase;

    public AddPlayerToTeamUseCaseTests()
    {
        _useCase = new AddPlayerToTeamUseCase(_teams, _players);
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenTeamIsMissing()
    {
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.ExecuteAsync(teamId: 999, "Pekka", 10));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenNumberIsTaken()
    {
        Team team = await _teams.AddAsync(Team.Create("A", "X", 10));
        await _useCase.ExecuteAsync(team.Id, "Pekka", 10);

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(team.Id, "Maija", 10));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenRosterIsFull()
    {
        Team team = await _teams.AddAsync(Team.Create("A", "X", 1));
        await _useCase.ExecuteAsync(team.Id, "Pekka", 10);

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(team.Id, "Maija", 11));
    }

    [Fact]
    public async Task ExecuteAsync_AddsPlayer_WhenValid()
    {
        Team team = await _teams.AddAsync(Team.Create("A", "X", 10));

        Player player = await _useCase.ExecuteAsync(team.Id, "Pekka", 10);

        Assert.Single(_players.Players);
        Assert.Equal(team.Id, player.TeamId);
    }
}
