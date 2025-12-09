public class Startup(IConfiguration configuration)
{
    public void ConfigurationService(IServiceCollection services)
    {
        services.ConfigureDatabase(configuration);
        services.ConfigureApplicationServices();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoint =>
        {
           endpoint.MapControllers(); 
        });
        app.UseHttpsRedirection();
    }

}