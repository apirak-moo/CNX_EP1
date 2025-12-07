public interface IPortfolioService
{
    Task<List<PortfolioResponse>> GetPortfolios(int limit = 3, int offset = 0, string? name = null);
    Task<PortfolioResponse?> GetPortfolio(Guid id);
    Task<PortfolioResponse> CreatePortfolio(PortfolioRequest request);
    Task<bool> UpdatePortfolio(Guid id, PortfolioRequest request);
    Task<bool> DeletePortfolio(Guid id);
}
