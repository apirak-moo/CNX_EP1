public class ToolService(IToolRepo toolRepo) : IToolService
{
    public async Task<ToolResponse> CreateTool(ToolRequest request)
    {
        return await toolRepo.CreateTool(request);
    }

    public async Task<bool> DeleteTool(int id)
    {
        return await toolRepo.DeleteTool(id);
    }

    public async Task<ToolResponse?> GetToolById(int id)
    {
        return await toolRepo.GetToolById(id);
    }

    public async Task<List<ToolResponse>> GetTools()
    {
        return await toolRepo.GetTools();
    }

    public async Task<bool> UpdateTool(int id, ToolRequest request)
    {
        return await toolRepo.UpdateTool(id, request);
    }
}
