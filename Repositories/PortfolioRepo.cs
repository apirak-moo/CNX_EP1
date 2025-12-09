using backend.Data;
using Microsoft.EntityFrameworkCore;

public class PortfolioRepo(AppDbContext context) : IPortfolioRepo
{
    private async Task<string?> UploadFile(IFormFile? file)
    {
        if(file is null)
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
        if(relativePath is null)
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

    public async Task<PortfolioResponse> CreatePortfolio(PortfolioRequest request)
    {
        string? imagePath = await UploadFile(request.Image); 
        var portfolio = new Portfolio 
        { 
            Title = request.Title,
            Description = request.Description,
            Image = imagePath,
            Link = request.Link,
            Status = request.Status,
            CategoryId = request.CategoryId,
        };
        var tools = await context.Tools.Where(t => request.ToolsId.Contains(t.Id)).ToListAsync();
        portfolio.Tools = tools;
        await context.Portfolios.AddAsync(portfolio);
        await context.SaveChangesAsync();

        await context.Entry(portfolio)
                    .Reference(p => p.Category)
                    .LoadAsync();

        await context.Entry(portfolio)
                    .Collection(p => p.Tools)
                    .LoadAsync();

        return new PortfolioResponse {
            Id = portfolio.Id,
            Title = portfolio.Title,
            Description = portfolio.Description,
            Image = portfolio.Image,
            Link = portfolio.Link,
            Status = portfolio.Status,
            Category = new CategoryResponse { Id = portfolio.Category.Id, Name = portfolio.Category.Name },
            Tools = [.. portfolio.Tools.Select(t => new ToolResponse { Id = t.Id, Name = t.Name, Logo = t.Logo })],
        };
    }

    public async Task<bool> DeletePortfolio(Guid id)
    {
        var portfolio = await context.Portfolios.FindAsync(id);
        if(portfolio is null)
        {
            return false;
        }
        await DeleteFile(portfolio.Image);
        context.Portfolios.Remove(portfolio);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<PortfolioResponse?> GetPortfolio(Guid id)
    {
        var portfolio = await context.Portfolios
                                        .Include(p => p.Category)
                                        .Include(p => p.Tools)
                                        .Select(p => new PortfolioResponse
                                        {
                                            Id = p.Id,
                                            Title = p.Title,
                                            Description = p.Description,
                                            Image = p.Image,
                                            Link = p.Link,
                                            Status = p.Status,
                                            Category = new CategoryResponse
                                            {
                                                Id = p.Category.Id,
                                                Name = p.Category.Name,
                                            },
                                            Tools = p.Tools.Select(t => new ToolResponse
                                            {
                                                Id = t.Id,
                                                Name = t.Name,
                                                Logo = t.Logo,
                                            }).ToList()
                                        }).FirstOrDefaultAsync();
        return portfolio;
    }

    public async Task<List<PortfolioResponse>> GetPortfolios(int limit = 3, int offset = 0, string? title = null)
    {
        IQueryable<Portfolio> query = context.Portfolios.AsNoTracking();
        if(!string.IsNullOrWhiteSpace(title))
        {
            string thaiCollation = "Thai_CI_AS";
            query = query.Where(p => EF.Functions.Collate(p.Title, thaiCollation).Contains(title));
        }
        return await query
                            .Include(p => p.Category)
                            .Include(p => p.Tools)
                            .OrderBy(p => p.Id)
                            .Skip(offset)
                            .Take(limit)
                            .Select(p => new PortfolioResponse
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Description = p.Description,
                                Image = p.Image,
                                Link = p.Link,
                                Status = p.Status,
                                Category =  new CategoryResponse
                                {
                                    Id = p.Category.Id,
                                    Name = p.Category.Name,
                                },
                                Tools = p.Tools.Select(t => new ToolResponse
                                {
                                    Id = t.Id,
                                    Name = t.Name,
                                    Logo = t.Logo,
                                }).ToList()
                            })
                            .ToListAsync();
    }

    public async Task<bool> UpdatePortfolio(Guid id, PortfolioRequest request)
    {
        var portfolio = await context.Portfolios.FindAsync(id);
        if(portfolio is null)
        {
            return false;
        }
        string? imagePath = await UploadFile(request.Image);
        await DeleteFile(portfolio.Image);
        portfolio.Title = request.Title;
        portfolio.Description = request.Description;
        portfolio.Image = imagePath;
        portfolio.Link = request.Link;
        portfolio.Status = request.Status;
        portfolio.CategoryId = request.CategoryId;
        var tools = await context.Tools.Where(t => request.ToolsId.Contains(t.Id)).ToListAsync();
        portfolio.Tools = tools;
        await context.SaveChangesAsync();
        return true;
    }
}