using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using LeagueHub.Domain.ValueObjects;

namespace LeagueHub.Tests.Domain;

// Domain-testit: ei fakea, ei HTTP:tä, ei edes rajapintaa.
public class MatchTests
{
    [Fact]
    public void Create_Throws_WhenHomeAndAwayAreTheSameTeam()
    {
        Assert.Throws<DomainException>(() =>
            Match.Create(homeTeamId: 1, awayTeamId: 1, scheduledAt: DateTime.UtcNow));
    }

    [Fact]
    public void RecordResult_Throws_WhenResultAlreadyExists()
    {
        Match match = Match.Create(1, 2, DateTime.UtcNow.AddDays(-1));
        Score score = Score.Create(1, 0);
        match.RecordResult(score, DateTime.UtcNow);

        Assert.Throws<DomainException>(() =>
            match.RecordResult(Score.Create(2, 2), DateTime.UtcNow));
    }

    [Fact]
    public void RecordResult_Throws_WhenMatchHasNotBeenPlayed()
    {
        DateTime kickoff = DateTime.UtcNow.AddDays(3);
        Match match = Match.Create(1, 2, kickoff);
        DateTime beforeKickoff = kickoff.AddHours(-1);

        Assert.Throws<DomainException>(() =>
            match.RecordResult(Score.Create(1, 0), beforeKickoff));
    }

    [Fact]
    public void RecordResult_SetsGoals_WhenMatchIsInThePast()
    {
        Match match = Match.Create(1, 2, DateTime.UtcNow.AddDays(-1));

        match.RecordResult(Score.Create(5, 2), DateTime.UtcNow);

        Assert.Equal(5, match.HomeGoals);
        Assert.Equal(2, match.AwayGoals);
    }
}
