using System.Security.Claims; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Authorize,Route("api/payments")]
public sealed class PaymentController(EComDbContext db,IPaymentService payments):ControllerBase
{
 [HttpPost("{orderId:long}/initiate")]
 public async Task<IActionResult> Initiate(long orderId){var userId=long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);var order=await db.Orders.SingleOrDefaultAsync(x=>x.Id==orderId&&x.UserId==userId);if(order is null)return NotFound();if(order.PaymentStatus=="PAID")return BadRequest(new{message="Order is already paid."});var result=await payments.InitiateAsync(order);return Ok(new{result.Gateway,result.Status,result.RedirectUrl,order.OrderNumber});}
}
