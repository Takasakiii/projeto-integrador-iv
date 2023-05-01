using JobsApi.Database;
using JobsApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Workers;

public class DatabaseWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DatabaseWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        
        await context.Database.MigrateAsync(cancellationToken: stoppingToken);
        await userService.CreateAdminUser();
    }
}