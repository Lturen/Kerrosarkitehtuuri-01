using LeagueHub.Domain.Exceptions;
using LeagueHub.Domain.ValueObjects;

namespace LeagueHub.Tests.Domain;

public class ScoreTests
{
    [Fact]
    public void Create_Throws_WhenHomeGoalsAreNegative()
    {
        Assert.Throws<DomainException>(() => Score.Create(-1, 0));
    }

    [Fact]
    public void Create_Throws_WhenAwayGoalsAreNegative()
    {
        Assert.Throws<DomainException>(() => Score.Create(0, -1));
    }

    [Fact]
    public void Create_ReturnsScore_WhenGoalsAreValid()
    {
        var score = Score.Create(5, 2);

        Assert.Equal(5, score.HomeGoals);
        Assert.Equal(2, score.AwayGoals);
    }
}
