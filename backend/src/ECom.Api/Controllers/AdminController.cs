using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "ADMIN")]
public sealed class AdminController(EComDbContext db) : ControllerBase
{
    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        return Ok(new
        {
            products = await db.Products.CountAsync(x => !x.IsDeleted),
            activeProducts = await db.Products.CountAsync(x => x.IsActive && !x.IsDeleted),
            categories = await db.Categories.CountAsync(x => x.IsActive)
        });
    }
}
