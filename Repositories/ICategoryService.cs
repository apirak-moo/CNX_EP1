public interface ICategoryService
{
    Task<List<CategoryResponse>> GetCategories();
    Task<CategoryResponse?> GetCategoryById(int id);
    Task<CategoryResponse> CreateCategory(CategoryRequest request);
    Task<bool> UpdateCategory(int id, CategoryRequest request);
    Task<bool> DeleteCategory(int id);
}
