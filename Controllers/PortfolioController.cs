using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("portfolios")]
public class PortfolioController(IPortfolioService portfolioService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetPortfolios([FromQuery] int limit = 3, [FromQuery] int offset = 0, [FromQuery] string? title = null)
    {
        return Ok(await portfolioService.GetPortfolios(limit, offset, title));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPortfolio(Guid id)
    {
        var portfolio = await portfolioService.GetPortfolio(id);
        if(portfolio is null)
        {
            return NotFound($"Portfolio with ID {id} not found");
        }
        return Ok(portfolio);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromForm] PortfolioRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var created = await portfolioService.CreatePortfolio(request);
        return CreatedAtAction(nameof(GetPortfolio), new { created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePortfolio(Guid id, [FromForm] PortfolioRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var updated = await portfolioService.UpdatePortfolio(id, request);
        if (updated)
        {
            return Ok("update success.");
        }
        return NotFound($"Portfolio with ID {id} not found");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePortfolio(Guid id) 
    { 
        var deleted = await portfolioService.DeletePortfolio(id);
        if (deleted)
        {
            return NoContent();
        }
        return NotFound($"Portfolio with ID {id} not found");
    }

}
