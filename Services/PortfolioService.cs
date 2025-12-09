
public class PortfolioService(IPortfolioRepo portfolioRepo) : IPortfolioService
{
    public async Task<PortfolioResponse> CreatePortfolio(PortfolioRequest request)
    {
        return await portfolioRepo.CreatePortfolio(request);
    }

    public async Task<bool> DeletePortfolio(Guid id)
    {
        return await portfolioRepo.DeletePortfolio(id);
    }

    public async Task<PortfolioResponse?> GetPortfolio(Guid id)
    {
        return await portfolioRepo.GetPortfolio(id);
    }

    public async Task<List<PortfolioResponse>> GetPortfolios(int limit = 3, int offset = 0, string? name = null)
    {
        return await portfolioRepo.GetPortfolios();
    }

    public async Task<bool> UpdatePortfolio(Guid id, PortfolioRequest request)
    {
        return await portfolioRepo.UpdatePortfolio(id, request);
    }
}
