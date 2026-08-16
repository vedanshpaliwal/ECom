using Microsoft.EntityFrameworkCore;

public sealed class EComDbContext(DbContextOptions<EComDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>(); public DbSet<Category> Categories => Set<Category>(); public DbSet<Product> Products => Set<Product>(); public DbSet<ProductImage> ProductImages => Set<ProductImage>(); public DbSet<Address> Addresses => Set<Address>(); public DbSet<Cart> Carts => Set<Cart>(); public DbSet<CartItem> CartItems => Set<CartItem>(); public DbSet<Order> Orders => Set<Order>(); public DbSet<OrderItem> OrderItems => Set<OrderItem>(); public DbSet<Payment> Payments => Set<Payment>(); public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    protected override void OnModelCreating(ModelBuilder m)
    {
        m.Entity<User>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>x.Email).IsUnique();e.Property(x=>x.Email).HasMaxLength(320).IsRequired();e.Property(x=>x.Name).HasMaxLength(150).IsRequired();});
        m.Entity<Category>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>x.Slug).IsUnique();e.Property(x=>x.Name).HasMaxLength(150).IsRequired();e.Property(x=>x.Slug).HasMaxLength(180).IsRequired();});
        m.Entity<Product>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>x.Sku).IsUnique();e.HasIndex(x=>new{x.CategoryId,x.IsActive});e.Property(x=>x.Name).HasMaxLength(200).IsRequired();e.Property(x=>x.Sku).HasMaxLength(80).IsRequired();e.Property(x=>x.Price).HasPrecision(18,2);e.Property(x=>x.CompareAtPrice).HasPrecision(18,2);e.HasOne(x=>x.Category).WithMany(x=>x.Products).HasForeignKey(x=>x.CategoryId);});
        m.Entity<ProductImage>(e=>{e.HasKey(x=>x.Id);e.Property(x=>x.Url).HasMaxLength(1000).IsRequired();e.HasOne(x=>x.Product).WithMany(x=>x.Images).HasForeignKey(x=>x.ProductId).OnDelete(DeleteBehavior.Cascade);});
        m.Entity<Address>(e=>{e.HasKey(x=>x.Id);e.Property(x=>x.FullName).HasMaxLength(150).IsRequired();e.Property(x=>x.PostalCode).HasMaxLength(10).IsRequired();});
        m.Entity<Cart>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>x.UserId).IsUnique();});
        m.Entity<CartItem>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>new{x.CartId,x.ProductId}).IsUnique();e.Property(x=>x.UnitPrice).HasPrecision(18,2);e.HasOne(x=>x.Cart).WithMany(x=>x.Items).HasForeignKey(x=>x.CartId).OnDelete(DeleteBehavior.Cascade);e.HasOne(x=>x.Product).WithMany().HasForeignKey(x=>x.ProductId);});
        m.Entity<Order>(e=>{e.HasKey(x=>x.Id);e.HasIndex(x=>x.OrderNumber).IsUnique();e.Property(x=>x.Subtotal).HasPrecision(18,2);e.Property(x=>x.Discount).HasPrecision(18,2);e.Property(x=>x.ShippingCharge).HasPrecision(18,2);e.Property(x=>x.TotalAmount).HasPrecision(18,2);});
        m.Entity<OrderItem>(e=>{e.HasKey(x=>x.Id);e.Property(x=>x.UnitPrice).HasPrecision(18,2);e.Property(x=>x.TotalPrice).HasPrecision(18,2);e.HasOne(x=>x.Order).WithMany(x=>x.Items).HasForeignKey(x=>x.OrderId).OnDelete(DeleteBehavior.Cascade);});
        m.Entity<Payment>(e=>{e.HasKey(x=>x.Id);e.Property(x=>x.Amount).HasPrecision(18,2);e.HasIndex(x=>x.TransactionId);});
        m.Entity<AuditLog>(e=>e.HasKey(x=>x.Id));
    }
}
public sealed class User { public long Id{get;set;} public string Name{get;set;}=""; public string Email{get;set;}=""; public string PasswordHash{get;set;}=""; public string Role{get;set;}="CUSTOMER"; public bool IsActive{get;set;}=true; public DateTimeOffset CreatedAt{get;set;}=DateTimeOffset.UtcNow; }
public sealed class Category { public long Id{get;set;} public string Name{get;set;}=""; public string Slug{get;set;}=""; public string? Description{get;set;} public string? ImageUrl{get;set;} public bool IsActive{get;set;}=true; public int DisplayOrder{get;set;} public DateTimeOffset CreatedAt{get;set;}=DateTimeOffset.UtcNow; public ICollection<Product> Products{get;set;}=[]; }
public sealed class Product { public long Id{get;set;} public long CategoryId{get;set;} public string Name{get;set;}=""; public string Sku{get;set;}=""; public string? Description{get;set;} public decimal Price{get;set;} public decimal? CompareAtPrice{get;set;} public int StockQuantity{get;set;} public bool IsActive{get;set;}=true; public bool IsDeleted{get;set;} public DateTimeOffset CreatedAt{get;set;}=DateTimeOffset.UtcNow; public DateTimeOffset UpdatedAt{get;set;}=DateTimeOffset.UtcNow; public Category Category{get;set;}=null!; public ICollection<ProductImage> Images{get;set;}=[]; }
public sealed class ProductImage { public long Id{get;set;} public long ProductId{get;set;} public string Url{get;set;}=""; public bool IsPrimary{get;set;} public int DisplayOrder{get;set;} public Product Product{get;set;}=null!; }
