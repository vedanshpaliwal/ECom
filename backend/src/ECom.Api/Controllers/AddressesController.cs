using System.Security.Claims; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Authorize,Route("api/addresses")]
public sealed class AddressesController(EComDbContext db):ControllerBase
{
 long UserId=>long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
 [HttpGet] public async Task<IActionResult> List()=>Ok(await db.Addresses.AsNoTracking().Where(x=>x.UserId==UserId).ToListAsync());
 [HttpPost] public async Task<IActionResult> Add(AddressRequest r){var a=new Address{UserId=UserId,FullName=r.FullName,Phone=r.Phone,Line1=r.Line1,Line2=r.Line2,City=r.City,State=r.State,PostalCode=r.PostalCode};db.Addresses.Add(a);await db.SaveChangesAsync();return Ok(a);}
}
public sealed record AddressRequest(string FullName,string Phone,string Line1,string? Line2,string City,string State,string PostalCode);
