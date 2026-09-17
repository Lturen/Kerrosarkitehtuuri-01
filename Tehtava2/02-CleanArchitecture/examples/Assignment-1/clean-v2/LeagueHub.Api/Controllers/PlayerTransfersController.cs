using LeagueHub.Api.Requests;
using LeagueHub.Application.Exceptions;
using LeagueHub.Application.UseCases.Players;
using LeagueHub.Domain.Entities;
using LeagueHub.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LeagueHub.Api.Controllers;

[ApiController]
[Route("api/players")]
public class PlayerTransfersController : ControllerBase
{
    private readonly TransferPlayerUseCase _transferPlayer;

    public PlayerTransfersController(TransferPlayerUseCase transferPlayer)
    {
        _transferPlayer = transferPlayer;
    }

    [HttpPost("{id}/transfer")]
    public async Task<IActionResult> Transfer(int id, TransferPlayerRequest request)
    {
        try
        {
            Player updated = await _transferPlayer.ExecuteAsync(id, request.TargetTeamId);
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
}
