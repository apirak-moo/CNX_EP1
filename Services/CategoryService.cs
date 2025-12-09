public class CategoryService(ICategoryRepo categoryRepo) : ICategoryService
{
    public async Task<CategoryResponse> CreateCategory(CategoryRequest request)
    {
        return await categoryRepo.CreateCategory(request);
    }

    public async Task<bool> DeleteCategory(int id)
    {
        return await categoryRepo.DeleteCategory(id);
    }

    public async Task<List<CategoryResponse>> GetCategories()
    {
        return await categoryRepo.GetCategories();
    }

    public async Task<CategoryResponse?> GetCategoryById(int id)
    {
        return await categoryRepo.GetCategoryById(id);
    }

    public async Task<bool> UpdateCategory(int id, CategoryRequest request)
    {
        return await categoryRepo.UpdateCategory(id, request);
    }
}
