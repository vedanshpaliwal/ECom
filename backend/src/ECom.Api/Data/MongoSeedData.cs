using Microsoft.EntityFrameworkCore;
public static class MongoSeedData
{
 public static async Task InitializeAsync(EComDbContext db,AuthService auth,IConfiguration config)
 {
  var categories=new[]{("woolen-flowers","Woolen Flowers","Handcrafted woolen flowers.",1),("flower-bouquets","Flower Bouquets","Gift-ready handmade bouquets.",2),("resin-art","Resin Art","Decorative handmade resin artwork.",3)};
  foreach(var c in categories)if(!await db.Categories.AnyAsync(x=>x.Slug==c.Item1))db.Categories.Add(new Category{Name=c.Item2,Slug=c.Item1,Description=c.Item3,DisplayOrder=c.Item4});
  await db.SaveChangesAsync();
  var categoryMap=await db.Categories.ToDictionaryAsync(x=>x.Slug,x=>x.Id);
  var products=new[]{
   ("Woolen Rose Bouquet","WOL-ROSE-001","Soft handcrafted woolen roses arranged as a keepsake bouquet.",799m,999m,25,"woolen-flowers"),
   ("Woolen Lavender Bunch","WOL-LAV-001","Delicate woolen lavender stems for home decor and gifting.",649m,799m,30,"woolen-flowers"),
   ("Pastel Flower Bouquet","BOU-PAS-001","A cheerful handmade bouquet in soft pastel tones.",1199m,1499m,15,"flower-bouquets"),
   ("Classic Red Gift Bouquet","BOU-RED-001","A timeless handmade red flower bouquet for special occasions.",1399m,1699m,12,"flower-bouquets"),
   ("Ocean Wave Resin Coaster Set","RES-OCE-001","Set of four handmade resin coasters with an ocean-inspired finish.",899m,1099m,20,"resin-art"),
   ("Resin Flower Bookmark","RES-BMK-001","Handmade resin bookmark with preserved-flower styling.",399m,499m,40,"resin-art")
  };
  foreach(var p in products)
  {
   if(await db.Products.AnyAsync(x=>x.Sku==p.Item2))continue;
   var product=new Product{CategoryId=categoryMap[p.Item7],Name=p.Item1,Sku=p.Item2,Description=p.Item3,Price=p.Item4,CompareAtPrice=p.Item5,StockQuantity=p.Item6,IsActive=true};
   db.Products.Add(product);await db.SaveChangesAsync();
   db.ProductImages.Add(new ProductImage{ProductId=product.Id,Url=$"https://placehold.co/900x900/F1E7DE/5A4638?text={Uri.EscapeDataString(p.Item1)}",IsPrimary=true,DisplayOrder=0});
   await db.SaveChangesAsync();
  }
  var email=config["Admin:Email"]?.Trim().ToLowerInvariant();var password=config["Admin:Password"];
  if(!string.IsNullOrWhiteSpace(email)&&!string.IsNullOrWhiteSpace(password)&&!await db.Users.AnyAsync(x=>x.Email==email))
  {db.Users.Add(new User{Name="Store Admin",Email=email,PasswordHash=auth.HashPassword(password),Role="ADMIN"});await db.SaveChangesAsync();}
 }
}
