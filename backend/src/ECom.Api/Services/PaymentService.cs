public interface IPaymentService { Task<PaymentInitiation> InitiateAsync(Order order); }
public sealed record PaymentInitiation(string Gateway,string Status,string? RedirectUrl);

// Paytm is deliberately isolated behind this interface. Production credentials and merchant configuration
// must come from environment/secret storage; no keys are committed to source control.
public sealed class PaytmPaymentService(IConfiguration configuration):IPaymentService
{
 public Task<PaymentInitiation> InitiateAsync(Order order)
 {
   var configured=configuration["Paytm:Enabled"]?.Equals("true",StringComparison.OrdinalIgnoreCase)==true;
   if(!configured) return Task.FromResult(new PaymentInitiation("PAYTM","CONFIGURATION_REQUIRED",null));
   return Task.FromResult(new PaymentInitiation("PAYTM","READY_FOR_GATEWAY_CONFIGURATION",null));
 }
}
