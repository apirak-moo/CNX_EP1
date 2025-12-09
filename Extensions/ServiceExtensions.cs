using backend.Data;
using Microsoft.EntityFrameworkCore;

public static class ServiceExtensions
{
    
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IToolService, ToolService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPortfolioService, PortfolioService>();
        return services;
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
    
}