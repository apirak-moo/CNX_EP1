using backend.Data;
using Microsoft.EntityFrameworkCore;

public class ToolService(AppDbContext context) : IToolService
{

    private async Task<string?> UploadFile(IFormFile? file)
    {
        if (file is null)
        {
            return null;
        }
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return $"/uploads/{fileName}";
    }

    private async Task DeleteFile(string? relativePath)
    {
        if (relativePath is null)
        {
            return;
        }
        if (string.IsNullOrWhiteSpace(relativePath))
            return;
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            relativePath.TrimStart('/')
        );
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    public async Task<ToolResponse> CreateTool(ToolRequest request)
    {
        string? logoPath = await UploadFile(request.Logo);
        Tool tool = new()
        {
            Name = request.Name,
            Logo = logoPath,
        };
        await context.Tools.AddAsync(tool);
        await context.SaveChangesAsync();
        return new ToolResponse { Id = tool.Id, Name = tool.Name, Logo = tool.Logo };
    }

    public async Task<bool> DeleteTool(int id)
    {
        var tool = await context.Tools.FindAsync(id);
        if(tool is null)
        {
            return false;
        }
        await DeleteFile(tool.Logo);
        context.Tools.Remove(tool);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<ToolResponse?> GetToolById(int id)
    {
        return await context.Tools
            .Select(t => new ToolResponse
            {
                Id = t.Id,
                Name = t.Name,
                Logo = t.Logo,
            })
            .Where(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<ToolResponse>> GetTools()
    {
        return await context.Tools
                            .AsNoTracking()
                            .OrderBy(t => t.Id)
                            .Select(t => new ToolResponse
                            {
                                Id = t.Id,
                                Name = t.Name,
                                Logo = t.Logo,
                            }).ToListAsync();
    }

    public async Task<bool> UpdateTool(int id, ToolRequest request)
    {
        var tool = await context.Tools.FindAsync(id);
        if (tool is null)
        {
            return false;
        }
        if(request.Logo is not null)
        {
            var logoPath = await UploadFile(request.Logo);
            tool.Logo = logoPath;
        }
        await DeleteFile(tool.Logo);
        tool.Name = request.Name;
        await context.SaveChangesAsync();
        return true;
    }

}
