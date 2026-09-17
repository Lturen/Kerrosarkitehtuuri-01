using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Tests.Domain;

public class TeamTests
{
    [Fact]
    public void Create_Throws_WhenNameIsEmpty()
    {
        Assert.Throws<DomainException>(() => Team.Create("", "Mikkeli", 10));
    }

    [Fact]
    public void Create_Throws_WhenNameIsWhitespace()
    {
        Assert.Throws<DomainException>(() => Team.Create("   ", "Mikkeli", 10));
    }

    [Fact]
    public void Create_Throws_WhenMaxRosterIsZeroOrNegative()
    {
        Assert.Throws<DomainException>(() => Team.Create("Maila", "Mikkeli", 0));
        Assert.Throws<DomainException>(() => Team.Create("Maila", "Mikkeli", -3));
    }

    [Fact]
    public void Create_TrimsNameAndCity()
    {
        Team team = Team.Create("  Maila  ", "  Mikkeli  ", 10);

        Assert.Equal("Maila", team.Name);
        Assert.Equal("Mikkeli", team.City);
    }

    [Fact]
    public void EnsureCanAddPlayer_Throws_WhenRosterIsFull()
    {
        Team team = Team.Create("Maila", "Mikkeli", 2);

        Assert.Throws<DomainException>(() => team.EnsureCanAddPlayer(2));
    }

    [Fact]
    public void EnsureCanAddPlayer_DoesNotThrow_WhenRosterHasRoom()
    {
        Team team = Team.Create("Maila", "Mikkeli", 2);

        team.EnsureCanAddPlayer(1);
    }
}
