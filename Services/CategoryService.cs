using backend.Data;
using Microsoft.EntityFrameworkCore;

public class CategoryService(AppDbContext context) : ICategoryService
{

    public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
    {
        Category category = new()
        {
            Name = request.Name
        };
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return new CategoryResponse { Id = category.Id, Name = category.Name };
    }

    public async Task<bool> DeleteCategory(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if(category is null)
        {
            return false;
        }
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<CategoryResponse>> GetCategories()
    {
        return await context.Categories
                            .OrderBy(c => c.Id)
                            .Select(c => new CategoryResponse { Id = c.Id, Name = c.Name })
                            .AsNoTracking()
                            .ToListAsync();
    }

    public async Task<CategoryResponse?> GetCategoryById(int id)
    {
        var category = await context.Categories
                                    .Select(c => new CategoryResponse { Id = c.Id, Name = c.Name })
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public async Task<bool> UpdateCategory(int id, CategoryRequest request)
    {
        var category = await context.Categories.FindAsync(id);
        if (category is null)
        {
            return false;
        }
        category.Name = request.Name;
        await context.SaveChangesAsync();
        return true;
    }

}
