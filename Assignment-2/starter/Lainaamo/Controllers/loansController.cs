using Lainaamo.Services;
using Lainaamo.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Lainaamo.Models;

namespace Lainaamo.Controllers;

[ApiController]
[Route("api/loans")]
public class loansController : ControllerBase
{

    private readonly ILoanservice _loans;

    public loansController(ILoanservice loans)
    {
        _loans = loans;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_loans.GetLoans());
    }

    [HttpGet("{id}")]
    public IActionResult GetLoanById(int id)
    {
        try
        {
            return Ok(_loans.GetById(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateLoan(LoanRequest request)
    {
        try
        {
            return Ok(_loans.Create(request.ItemId, request.BorrowerName));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/return")]
    public IActionResult ReturnLoan(int id)
    {
        try
        {
            return Ok(_loans.Return(id));
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteLoan(int id)
    {
        try
        {
            _loans.Delete(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
