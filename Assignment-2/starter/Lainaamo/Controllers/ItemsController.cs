using Lainaamo.Exceptions;
using Lainaamo.Models;
using Lainaamo.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lainaamo.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    public readonly IItemService _items;

    public ItemsController(IItemService items)
    {
        _items = items;
    }
    [HttpGet("items")]
    public IActionResult Get()
    {
        return Ok(_items.GetItems());
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_items.GetById(id));
    }

    [HttpPost("items")]

    public IActionResult Create(Item item)
    {
        try
        {
            Item created = _items.Create(item.Id, item.Name);
            return Created($"/api/items/{created.Id}", created);
        }
        catch (ItemCantBeCreated ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }

    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _items.Delete(id);
            return NoContent();
        }
        catch (ItemCantBeDeleted ex)
        {
            return BadRequest(ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
