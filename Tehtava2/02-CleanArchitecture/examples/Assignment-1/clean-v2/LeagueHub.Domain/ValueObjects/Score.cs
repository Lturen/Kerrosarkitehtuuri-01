using LeagueHub.Domain.Exceptions;

namespace LeagueHub.Domain.ValueObjects;

// Value object: kaksi maalia, jotka kuuluvat yhteen. Ei Id:tä — 5–2 on 5–2.
public sealed class Score
{
    public int HomeGoals { get; }
    public int AwayGoals { get; }

    private Score(int homeGoals, int awayGoals)
    {
        HomeGoals = homeGoals;
        AwayGoals = awayGoals;
    }

    public static Score Create(int homeGoals, int awayGoals)
    {
        if (homeGoals < 0 || awayGoals < 0)
        {
            throw new DomainException("Maalit eivät voi olla negatiivisia.");
        }

        return new Score(homeGoals, awayGoals);
    }
}
