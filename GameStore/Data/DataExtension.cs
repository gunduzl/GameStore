using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public static class DataExtension
{
    public static void MigrateDb(this WebApplication app){
        using var scope = app.Services.CreateScope();
        var DbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        DbContext.Database.Migrate();
    }
}
