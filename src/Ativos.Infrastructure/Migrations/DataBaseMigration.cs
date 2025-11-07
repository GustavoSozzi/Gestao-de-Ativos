using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ativos.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public async static Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<AtivosDbContext>();
        
        await dbContext.Database.MigrateAsync();
    }
}