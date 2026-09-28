using Microsoft.AspNetCore.Mvc;
using LeagueHub.Models;
using LeagueHub.Services;
using LeagueHub.Expections;

namespace LeagueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayersController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_playerService.GetAllPlayers());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var player = _playerService.GetPlayerById(id);

                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }

                return Ok(player);
            }
            catch (PlayerExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}/customise")]
        public IActionResult CustomiseById(int id)
        {
            try
            {
                var player = _playerService.CustomisePlayerById(id);

                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }

                return Ok(player);
            }
            catch (PlayerExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Player player)
        {
            try
            {
                var created = _playerService.AddPlayer(player);
                return CreatedAtAction(nameof(GetById), new { id = created.PlayerNumber }, created);
            }
            catch (PlayerExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                var player = _playerService.GetPlayerById(id);

                if (player == null)
                {
                    return NotFound($"Player with ID {id} not found.");
                }

                _playerService.RemovePlayer(player);
                return NoContent();
            }
            catch (PlayerExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
