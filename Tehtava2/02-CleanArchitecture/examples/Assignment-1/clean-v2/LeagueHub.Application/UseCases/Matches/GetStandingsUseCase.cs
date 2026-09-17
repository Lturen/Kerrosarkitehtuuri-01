using LeagueHub.Application.Models;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Interfaces;

namespace LeagueHub.Application.UseCases.Matches;

// Sarjataulukko on lukumalli: lasketaan otteluista, ei tallenneta.
// Pistelasku ei kuulu Match-entiteettiin — yksi ottelu ei tiedä koko sarjaa.
public class GetStandingsUseCase
{
    private readonly ITeamRepository _teams;
    private readonly IMatchRepository _matches;

    public GetStandingsUseCase(ITeamRepository teams, IMatchRepository matches)
    {
        _teams = teams;
        _matches = matches;
    }

    public async Task<List<Standing>> ExecuteAsync()
    {
        List<Team> teams = await _teams.GetAllAsync();
        List<Match> matches = await _matches.GetAllAsync();

        List<Standing> rows = [];

        foreach (Team team in teams)
        {
            int wins = 0;
            int draws = 0;
            int losses = 0;

            foreach (Match match in matches)
            {
                if (!match.HasResult)
                {
                    continue;
                }

                bool isHome = match.HomeTeamId == team.Id;
                bool isAway = match.AwayTeamId == team.Id;

                if (!isHome && !isAway)
                {
                    continue;
                }

                int ours = isHome ? match.HomeGoals!.Value : match.AwayGoals!.Value;
                int theirs = isHome ? match.AwayGoals!.Value : match.HomeGoals!.Value;

                if (ours > theirs)
                {
                    wins++;
                }
                else if (ours == theirs)
                {
                    draws++;
                }
                else
                {
                    losses++;
                }
            }

            rows.Add(new Standing
            {
                TeamId = team.Id,
                TeamName = team.Name,
                Wins = wins,
                Draws = draws,
                Losses = losses,
                Points = wins * 3 + draws
            });
        }

        return rows.OrderByDescending(r => r.Points).ToList();
    }
}
