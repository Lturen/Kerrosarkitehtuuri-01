using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Tests.Domain;

public class PlayerTests
{
    [Fact]
    public void Create_Throws_WhenNameIsEmpty()
    {
        Assert.Throws<DomainException>(() => Player.Create(1, "", 10));
    }

    [Fact]
    public void Create_Throws_WhenNumberIsZeroOrNegative()
    {
        Assert.Throws<DomainException>(() => Player.Create(1, "Pekka", 0));
        Assert.Throws<DomainException>(() => Player.Create(1, "Pekka", -7));
    }

    [Fact]
    public void Create_ReturnsPlayer_WhenValid()
    {
        Player player = Player.Create(1, "  Pekka  ", 10);

        Assert.Equal("Pekka", player.Name);
        Assert.Equal(10, player.Number);
        Assert.Equal(1, player.TeamId);
    }

    [Fact]
    public void TransferTo_Throws_WhenTargetIsTheSameTeam()
    {
        Player player = Player.Create(1, "Pekka", 10);

        Assert.Throws<DomainException>(() => player.TransferTo(1));
    }

    [Fact]
    public void TransferTo_ChangesTeamId_WhenTargetIsDifferent()
    {
        Player player = Player.Create(1, "Pekka", 10);

        player.TransferTo(2);

        Assert.Equal(2, player.TeamId);
    }
}
