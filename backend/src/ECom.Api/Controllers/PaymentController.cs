using System.Security.Claims; using Microsoft.AspNetCore.Authorization; using Microsoft.AspNetCore.Mvc; using Microsoft.EntityFrameworkCore;
[ApiController,Authorize,Route("api/payments")]
public sealed class PaymentController(EComDbContext db,IPaymentService payments):ControllerBase
{
 [HttpPost("{orderId:long}/initiate")]
 public async Task<IActionResult> Initiate(long orderId){var uid=long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);var order=await db.Orders.SingleOrDefaultAsync(x=>x.Id==orderId&&x.UserId==uid);if(order is null)return NotFound();if(order.PaymentStatus=="PAID")return BadRequest(new{message="Order is already paid."});var result=await payments.InitiateAsync(order);return Ok(new{result.Gateway,result.Status,result.RedirectUrl,order.OrderNumber});}
 [AllowAnonymous,HttpPost("paytm/callback")]
 public async Task<IActionResult> Callback([FromQuery]string orderNumber,[FromBody]Dictionary<string,string> callback){var order=await db.Orders.SingleOrDefaultAsync(x=>x.OrderNumber==orderNumber);if(order is null)return NotFound();var result=await payments.VerifyAsync(orderNumber,callback);var p=await db.Payments.SingleOrDefaultAsync(x=>x.OrderId==order.Id);if(p is null){p=new Payment{OrderId=order.Id,Amount=order.TotalAmount};db.Payments.Add(p);}p.Status=result.Status;p.TransactionId=result.TransactionId;p.GatewayResponse=System.Text.Json.JsonSerializer.Serialize(callback);if(result.Success){order.PaymentStatus="PAID";order.OrderStatus="PROCESSING";}await db.SaveChangesAsync();return Ok(new{success=result.Success,status=order.PaymentStatus});}
}
