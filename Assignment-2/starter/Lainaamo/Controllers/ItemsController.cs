using Lainaamo.Services;
using Lainaamo.Models;
using Lainaamo.Exceptions;
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
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_items.GetItems());
    }
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_items.GetById(id));
    }

    [HttpPost]

    public IActionResult Create(Item item)
    {
        return Ok(_items.Create(item));
    }



}

