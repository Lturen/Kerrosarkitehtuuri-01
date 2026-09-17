using LeagueHub.Api.Requests;
using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Matches;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LeagueHub.Api.Controllers;

[ApiController]
[Route("api/matches")]
public class MatchesController : ControllerBase
{
    private readonly GetMatchesUseCase _getMatches;
    private readonly CreateMatchUseCase _createMatch;
    private readonly RecordMatchResultUseCase _recordResult;
    private readonly GetStandingsUseCase _getStandings;

    public MatchesController(
        GetMatchesUseCase getMatches,
        CreateMatchUseCase createMatch,
        RecordMatchResultUseCase recordResult,
        GetStandingsUseCase getStandings)
    {
        _getMatches = getMatches;
        _createMatch = createMatch;
        _recordResult = recordResult;
        _getStandings = getStandings;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _getMatches.ExecuteAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMatchRequest request)
    {
        try
        {
            Match created = await _createMatch.ExecuteAsync(
                request.HomeTeamId, request.AwayTeamId, request.ScheduledAt);

            return Created($"/api/matches/{created.Id}", created);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("{id}/result")]
    public async Task<IActionResult> RecordResult(int id, RecordResultRequest request)
    {
        try
        {
            Match updated = await _recordResult.ExecuteAsync(
                id, request.HomeGoals, request.AwayGoals);

            return Ok(updated);
        }
        catch (DomainException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("/api/standings")]
    public async Task<IActionResult> GetStandings()
    {
        return Ok(await _getStandings.ExecuteAsync());
    }
}
