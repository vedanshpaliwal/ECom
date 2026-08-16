using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/products")]
public sealed class ProductsController(EComDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var products = await db.Products
            .AsNoTracking()
            .Include(x => x.Images)
            .Where(x => x.IsActive && !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Sku,
                x.Description,
                x.Price,
                x.CompareAtPrice,
                x.StockQuantity,
                category = x.Category.Name,
                images = x.Images.OrderBy(i => i.DisplayOrder).Select(i => i.Url)
            })
            .ToListAsync(cancellationToken);

        return Ok(products);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetProduct(long id, CancellationToken cancellationToken)
    {
        var product = await db.Products
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Images)
            .Where(x => x.Id == id && x.IsActive && !x.IsDeleted)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Sku,
                x.Description,
                x.Price,
                x.CompareAtPrice,
                x.StockQuantity,
                category = x.Category.Name,
                images = x.Images.OrderBy(i => i.DisplayOrder).Select(i => i.Url)
            })
            .SingleOrDefaultAsync(cancellationToken);

        return product is null ? NotFound() : Ok(product);
    }
}
