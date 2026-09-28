using Microsoft.AspNetCore.Mvc;
using LeagueHub.Models;
using LeagueHub.Services;
using LeagueHub.Expections;

namespace LeagueHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeagueTablesController : ControllerBase
    {
        private readonly ILeagueTableService _leagueTableService;

        public LeagueTablesController(ILeagueTableService leagueTableService)
        {
            _leagueTableService = leagueTableService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_leagueTableService.GetAllEntries());
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
                var entry = _leagueTableService.GetEntryById(id);

                if (entry == null)
                {
                    return NotFound($"League table entry with ID {id} not found.");
                }

                return Ok(entry);
            }
            catch (LeagueTableExpection ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] LeagueTable entry)
        {
            try
            {
                var created = _leagueTableService.AddEntry(entry);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (LeagueTableExpection ex)
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
                var entry = _leagueTableService.GetEntryById(id);

                if (entry == null)
                {
                    return NotFound($"League table entry with ID {id} not found.");
                }

                _leagueTableService.RemoveEntry(entry);
                return NoContent();
            }
            catch (LeagueTableExpection ex)
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
