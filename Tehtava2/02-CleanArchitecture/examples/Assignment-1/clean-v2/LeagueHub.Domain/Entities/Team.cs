using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Domain.Entities;

public class Team
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public int MaxRoster { get; private set; }

    // EF Core tarvitsee parametrittoman konstruktorin — private riittää sille.
    private Team() { }

    public static Team Create(string name, string city, int maxRoster)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new DomainException("Joukkueen nimi on pakollinen.");
        }

        if (maxRoster <= 0)
        {
            throw new DomainException("Rosterin ylärajan pitää olla positiivinen.");
        }

        return new Team
        {
            Name = name.Trim(),
            City = city?.Trim() ?? string.Empty,
            MaxRoster = maxRoster
        };
    }

    // Rosterin yläraja on joukkueen oma invariantti.
    public void EnsureCanAddPlayer(int currentPlayerCount)
    {
        if (currentPlayerCount >= MaxRoster)
        {
            throw new DomainException("Roster on täynnä.");
        }
    }

    // Vain sama assembly ja testit (InternalsVisibleTo) saavat asettaa tunnisteen.
    internal void AssignId(int id) => Id = id;
}
