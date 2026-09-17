using LeagueHub.Application.Exceptions;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;
using LeagueHub.Domain.ValueObjects;

namespace LeagueHub.Application.UseCases.Matches;

public class RecordMatchResultUseCase
{
    private readonly IMatchRepository _matches;

    public RecordMatchResultUseCase(IMatchRepository matches)
    {
        _matches = matches;
    }

    public async Task<Match> ExecuteAsync(int matchId, int homeGoals, int awayGoals)
    {
        Match? match = await _matches.GetByIdAsync(matchId);

        if (match is null)
        {
            throw new NotFoundException("Ottelua ei löydy.");
        }

        Score score = Score.Create(homeGoals, awayGoals);
        match.RecordResult(score, DateTime.UtcNow);
        await _matches.UpdateAsync(match);
        return match;
    }
}
