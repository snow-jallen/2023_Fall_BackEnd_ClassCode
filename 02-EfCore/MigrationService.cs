using Microsoft.EntityFrameworkCore;
using Recapi.Data;

namespace EfCoreDemo;

public class MigrationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public MigrationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<RecipeContext>();

            await db.Database.MigrateAsync(stoppingToken);
        }
    }
}
