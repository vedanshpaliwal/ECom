public interface INotificationService
{
    Task NotifyOrderAsync(Order order, CancellationToken cancellationToken = default);
}

public sealed class NotificationService(IConfiguration configuration, IHttpClientFactory httpClientFactory) : INotificationService
{
    public async Task NotifyOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        // Email/WhatsApp providers are intentionally configured through secrets.
        // This method is the single application boundary for post-order notifications.
        await Task.CompletedTask;
    }
}
