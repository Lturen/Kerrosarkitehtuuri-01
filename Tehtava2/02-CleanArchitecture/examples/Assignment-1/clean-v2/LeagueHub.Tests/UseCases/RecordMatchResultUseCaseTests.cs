using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Matches;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Tests.Fakes;

namespace LeagueHub.Tests.UseCases;

public class RecordMatchResultUseCaseTests
{
    private readonly FakeMatchRepository _matches = new();
    private readonly RecordMatchResultUseCase _useCase;

    public RecordMatchResultUseCaseTests()
    {
        _useCase = new RecordMatchResultUseCase(_matches);
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenMatchIsMissing()
    {
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.ExecuteAsync(matchId: 999, homeGoals: 1, awayGoals: 0));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenMatchIsInTheFuture()
    {
        Match match = await _matches.AddAsync(
            Match.Create(1, 2, DateTime.UtcNow.AddDays(3)));

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(match.Id, 1, 0));
    }

    [Fact]
    public async Task ExecuteAsync_Throws_WhenGoalsAreNegative()
    {
        Match match = await _matches.AddAsync(
            Match.Create(1, 2, DateTime.UtcNow.AddDays(-1)));

        await Assert.ThrowsAsync<DomainException>(() =>
            _useCase.ExecuteAsync(match.Id, -1, 0));
    }

    [Fact]
    public async Task ExecuteAsync_RecordsResult_WhenMatchIsPlayed()
    {
        Match match = await _matches.AddAsync(
            Match.Create(1, 2, DateTime.UtcNow.AddDays(-1)));

        Match updated = await _useCase.ExecuteAsync(match.Id, 5, 2);

        Assert.Equal(5, updated.HomeGoals);
        Assert.Equal(2, updated.AwayGoals);
    }
}
