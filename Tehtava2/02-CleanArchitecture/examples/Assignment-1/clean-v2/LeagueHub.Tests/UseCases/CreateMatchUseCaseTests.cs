using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Matches;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Tests.Fakes;

namespace LeagueHub.Tests.UseCases;

public class CreateMatchUseCaseTests
{
    private readonly FakeTeamRepository _teams = new();
    private readonly FakeMatchRepository _matches = new();
    private readonly CreateMatchUseCase _useCase;

    public CreateMatchUseCaseTests()
    {
        _useCase = new CreateMatchUseCase(_teams, _matches);
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenTeamIsMissing()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.ExecuteAsync(home.Id, awayTeamId: 999, DateTime.UtcNow.AddDays(1)));
    }

    // Use case ei tarkista "sama joukkue" -sääntöä — domain heittää silti.
    [Fact]
    public async Task ExecuteAsync_Throws_WhenHomeAndAwayAreTheSame()
    {
        Team team = await _teams.AddAsync(Team.Create("A", "X", 10));

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(team.Id, team.Id, DateTime.UtcNow.AddDays(1)));
    }

    [Fact]
    public async Task ExecuteAsync_AddsMatch_WhenBothTeamsExist()
    {
        Team home = await _teams.AddAsync(Team.Create("A", "X", 10));
        Team away = await _teams.AddAsync(Team.Create("B", "Y", 10));

        Match created = await _useCase.ExecuteAsync(home.Id, away.Id, DateTime.UtcNow.AddDays(1));

        Assert.Single(_matches.Matches);
        Assert.Equal(home.Id, created.HomeTeamId);
        Assert.Equal(away.Id, created.AwayTeamId);
    }
}
