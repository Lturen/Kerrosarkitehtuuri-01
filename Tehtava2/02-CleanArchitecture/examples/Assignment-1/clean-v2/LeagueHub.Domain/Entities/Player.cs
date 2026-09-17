using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Domain.Entities;

// Player on oma entiteetti, jolla on TeamId — ei List<Player> Teamin sisällä.
public class Player
{
    public int Id { get; private set; }
    public int TeamId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Number { get; private set; }

    private Player() { }

    public static Player Create(int teamId, string name, int number)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Pelaajan nimi on pakollinen.");
        }

        if (number <= 0)
        {
            throw new DomainException("Pelinumeron pitää olla positiivinen.");
        }

        return new Player
        {
            TeamId = teamId,
            Name = name.Trim(),
            Number = number
        };
    }

    // Sama joukkue ei ole siirto — se on pelaajan omaa dataa, ei hakua.
    public void TransferTo(int targetTeamId)
    {
        if (targetTeamId == TeamId)
        {
            throw new DomainException("Pelaaja on jo tässä joukkueessa.");
        }

        TeamId = targetTeamId;
    }

    internal void AssignId(int id) => Id = id;
}
