using Microsoft.AspNetCore.Mvc;
using LeagueHub.Models;
using LeagueHub.Services;
using LeagueHub.Expections;

namespace LeagueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_teamService.GetAllTeams());
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
                var team = _teamService.GetTeamById(id);

                if (team == null)
                {
                    return NotFound($"Team with ID {id} not found.");
                }

                return Ok(team);
            }
            catch (TeamExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Team team)
        {
            try
            {
                var created = _teamService.AddTeam(team);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (TeamExpection ex)
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
                var team = _teamService.GetTeamById(id);

                if (team == null)
                {
                    return NotFound($"Team with ID {id} not found.");
                }

                _teamService.RemoveTeam(team);
                return NoContent();
            }
            catch (TeamExpection ex)
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
