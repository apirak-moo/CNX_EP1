public interface IToolRepo
{
    Task<List<ToolResponse>> GetTools();
    Task<ToolResponse?> GetToolById(int id);
    Task<ToolResponse> CreateTool(ToolRequest request);
    Task<bool> UpdateTool(int id, ToolRequest request);
    Task<bool> DeleteTool(int id);
}