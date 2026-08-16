using Microsoft.EntityFrameworkCore;
public static class SeedData
{
 public static async Task InitializeAsync(EComDbContext db,AuthService auth,IConfiguration config)
 {
  await db.Database.EnsureCreatedAsync();
  if(!await db.Categories.AnyAsync()) { db.Categories.AddRange(new Category{Name="Woolen Flowers",Slug="woolen-flowers",Description="Handcrafted woolen flowers.",DisplayOrder=1},new Category{Name="Flower Bouquets",Slug="flower-bouquets",Description="Gift-ready handmade bouquets.",DisplayOrder=2},new Category{Name="Resin Art",Slug="resin-art",Description="Decorative handmade resin artwork.",DisplayOrder=3}); await db.SaveChangesAsync(); }
  var email=config["Admin:Email"]?.Trim().ToLowerInvariant(); var password=config["Admin:Password"]; if(!string.IsNullOrWhiteSpace(email)&&!string.IsNullOrWhiteSpace(password)&&!await db.Users.AnyAsync(x=>x.Email==email)){db.Users.Add(new User{Name="Store Admin",Email=email,PasswordHash=auth.HashPassword(password),Role="ADMIN"});await db.SaveChangesAsync();}
 }
}
