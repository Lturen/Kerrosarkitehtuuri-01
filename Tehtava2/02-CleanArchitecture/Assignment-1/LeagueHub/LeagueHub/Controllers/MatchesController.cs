using Microsoft.AspNetCore.Mvc;
using LeagueHub.Models;
using LeagueHub.Services;
using LeagueHub.Expections;

namespace LeagueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_matchService.GetAllMatches());
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
                var match = _matchService.GetMatchById(id);

                if (match == null)
                {
                    return NotFound($"Match with ID {id} not found.");
                }

                return Ok(match);
            }
            catch (MatchExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Match match)
        {
            try
            {
                var created = _matchService.AddMatch(match);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (MatchExpection ex)
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
                var match = _matchService.GetMatchById(id);

                if (match == null)
                {
                    return NotFound($"Match with ID {id} not found.");
                }

                _matchService.RemoveMatch(match);
                return NoContent();
            }
            catch (MatchExpection ex)
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
