using Microsoft.EntityFrameworkCore;
public static class SeedData
{
    public static async Task InitializeAsync(EComDbContext db)
    {
        await db.Database.MigrateAsync();
        if (!await db.Categories.AnyAsync())
        {
            db.Categories.AddRange(
                new Category { Name="Woolen Flowers", Slug="woolen-flowers", Description="Handcrafted woolen flowers.", DisplayOrder=1 },
                new Category { Name="Flower Bouquets", Slug="flower-bouquets", Description="Gift-ready handmade bouquets.", DisplayOrder=2 },
                new Category { Name="Resin Art", Slug="resin-art", Description="Decorative handmade resin artwork.", DisplayOrder=3 });
            await db.SaveChangesAsync();
        }
    }
}
