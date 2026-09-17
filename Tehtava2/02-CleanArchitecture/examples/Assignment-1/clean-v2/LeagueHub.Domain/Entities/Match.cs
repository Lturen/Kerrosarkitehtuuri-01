using LeagueHub.Domain.Exceptions;
using LeagueHub.Domain.ValueObjects;

namespace LeagueHub.Domain.Entities;

public class Match
{
    public int Id { get; private set; }
    public int HomeTeamId { get; private set; }
    public int AwayTeamId { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int? HomeGoals { get; private set; }
    public int? AwayGoals { get; private set; }

    public bool HasResult => HomeGoals is not null;

    private Match() { }

    public static Match Create(int homeTeamId, int awayTeamId, DateTime scheduledAt)
    {
        if (homeTeamId == awayTeamId)
        {
            throw new DomainException("Koti ja vieras eivät voi olla sama joukkue.");
        }

        return new Match
        {
            HomeTeamId = homeTeamId,
            AwayTeamId = awayTeamId,
            ScheduledAt = scheduledAt
        };
    }

    // now on parametri, jotta sääntö on testattava ilman oikeaa kelloa.
    public void RecordResult(Score score, DateTime now)
    {
        if (HasResult)
        {
            throw new DomainException("Tulos on jo kirjattu.");
        }

        if (ScheduledAt > now)
        {
            throw new DomainException("Ottelu ei ole vielä pelattu.");
        }

        HomeGoals = score.HomeGoals;
        AwayGoals = score.AwayGoals;
    }

    internal void AssignId(int id) => Id = id;
}
