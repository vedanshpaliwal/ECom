using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Authorize(Roles="ADMIN"),Route("api/admin/products")]
public sealed class AdminProductsController(EComDbContext db):ControllerBase
{
 [HttpPost] public async Task<IActionResult> Create(ProductRequest r){if(await db.Products.AnyAsync(x=>x.Sku==r.Sku))return Conflict(new{message="SKU already exists."});var p=new Product{CategoryId=r.CategoryId,Name=r.Name.Trim(),Sku=r.Sku.Trim(),Description=r.Description,Price=r.Price,CompareAtPrice=r.CompareAtPrice,StockQuantity=r.StockQuantity};db.Products.Add(p);await db.SaveChangesAsync();return Created($"api/products/{p.Id}",new{p.Id});}
 [HttpPut("{id:long}")] public async Task<IActionResult> Update(long id,ProductRequest r){var p=await db.Products.FindAsync(id);if(p is null)return NotFound();p.Name=r.Name.Trim();p.CategoryId=r.CategoryId;p.Description=r.Description;p.Price=r.Price;p.CompareAtPrice=r.CompareAtPrice;p.StockQuantity=r.StockQuantity;p.IsActive=r.IsActive;p.UpdatedAt=DateTimeOffset.UtcNow;await db.SaveChangesAsync();return Ok();}
 [HttpDelete("{id:long}")] public async Task<IActionResult> Delete(long id){var p=await db.Products.FindAsync(id);if(p is null)return NotFound();p.IsDeleted=true;p.IsActive=false;await db.SaveChangesAsync();return NoContent();}
}
public sealed record ProductRequest(long CategoryId,string Name,string Sku,string? Description,decimal Price,decimal? CompareAtPrice,int StockQuantity,bool IsActive=true);
