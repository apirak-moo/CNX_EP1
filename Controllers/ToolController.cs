using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("tools")]
public class ToolController(IToolService toolService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetTools()
    {
        return Ok(await toolService.GetTools());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTool(int id)
    {
        var tool = await toolService.GetToolById(id);
        if(tool is null)
        {
            return NotFound(new { message = $"Tool with ID {id} not found" });
        }
        return Ok(tool);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTool([FromForm] ToolRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var created = await toolService.CreateTool(request);
        return CreatedAtAction(nameof(GetTool), new { created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTool(int id,[FromForm] ToolRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var updated = await toolService.UpdateTool(id, request);
        if (updated)
        {
            return Ok("update success.");
        }
        return NotFound(new { message = $"Tool with ID {id} not found" });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteTool(int id)
    {
        var deleted = await toolService.DeleteTool(id);
        if (deleted)
        {
            return NoContent();
        }
        return NotFound(new { message = $"Tool with ID {id} not found" });
    }

}
