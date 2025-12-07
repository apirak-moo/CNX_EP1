using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        return Ok(await categoryService.GetCategories());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await categoryService.GetCategoryById(id);
        if(category is null)
        {
            return NotFound($"Category with ID {id} not found");
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var created = await categoryService.CreateCategory(request);
        return CreatedAtAction(nameof(GetCategory), new { created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCategory(int id,[FromBody] CategoryRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var updated = await categoryService.UpdateCategory(id, request);
        if (updated)
        {
            return Ok("update success.");
        }
        return NotFound($"Category with ID {id} not found");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var deleted = await categoryService.DeleteCategory(id);
        if (deleted)
        {
            return NoContent();
        }
        return NotFound($"Category with ID {id} not found");
    }

}
