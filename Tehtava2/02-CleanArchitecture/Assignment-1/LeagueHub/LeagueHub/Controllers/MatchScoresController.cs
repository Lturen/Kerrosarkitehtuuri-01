using Microsoft.AspNetCore.Mvc;
using LeagueHub.Models;
using LeagueHub.Services;
using LeagueHub.Expections;

namespace LeagueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchScoresController : ControllerBase
    {
        private readonly IMatchScoreService _matchScoreService;

        public MatchScoresController(IMatchScoreService matchScoreService)
        {
            _matchScoreService = matchScoreService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_matchScoreService.GetAllMatchScores());
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
                var matchScore = _matchScoreService.GetMatchScoreById(id);

                if (matchScore == null)
                {
                    return NotFound($"Match score with ID {id} not found.");
                }

                return Ok(matchScore);
            }
            catch (MatchScoreExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] MatchScore matchScore)
        {
            try
            {
                var created = _matchScoreService.AddMatchScore(matchScore);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (MatchScoreExpection ex)
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
                var matchScore = _matchScoreService.GetMatchScoreById(id);

                if (matchScore == null)
                {
                    return NotFound($"Match score with ID {id} not found.");
                }

                _matchScoreService.RemoveMatchScore(matchScore);
                return NoContent();
            }
            catch (MatchScoreExpection ex)
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
