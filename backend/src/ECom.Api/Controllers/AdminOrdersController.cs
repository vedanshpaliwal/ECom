using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Authorize(Roles="ADMIN"),Route("api/admin/orders")]
public sealed class AdminOrdersController(EComDbContext db):ControllerBase
{
 [HttpGet] public async Task<IActionResult> List([FromQuery]string? status=null,[FromQuery]string? paymentStatus=null){var q=db.Orders.AsNoTracking().Include(x=>x.Items).AsQueryable();if(!string.IsNullOrWhiteSpace(status))q=q.Where(x=>x.OrderStatus==status);if(!string.IsNullOrWhiteSpace(paymentStatus))q=q.Where(x=>x.PaymentStatus==paymentStatus);return Ok(await q.OrderByDescending(x=>x.CreatedAt).Select(x=>new{x.Id,x.OrderNumber,x.UserId,x.TotalAmount,x.PaymentStatus,x.OrderStatus,x.CreatedAt,x.Items}).ToListAsync());}
 [HttpPatch("{id:long}/status")] public async Task<IActionResult> Update(long id,UpdateStatusRequest r){var o=await db.Orders.FindAsync(id);if(o is null)return NotFound();var old=o.OrderStatus;o.OrderStatus=r.Status;db.AuditLogs.Add(new AuditLog{Action="ORDER_STATUS_UPDATED",EntityType="ORDER",EntityId=o.OrderNumber,OldValue=old,NewValue=r.Status});await db.SaveChangesAsync();return Ok(new{status=o.OrderStatus});}
}
public sealed record UpdateStatusRequest(string Status);
