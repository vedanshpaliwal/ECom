using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Route("api/categories")]
public sealed class CategoriesController(EComDbContext db):ControllerBase
{
 [HttpGet] public async Task<IActionResult> List()=>Ok(await db.Categories.AsNoTracking().Where(x=>x.IsActive).OrderBy(x=>x.DisplayOrder).Select(x=>new{x.Id,x.Name,x.Slug,x.Description,x.ImageUrl}).ToListAsync());
}
