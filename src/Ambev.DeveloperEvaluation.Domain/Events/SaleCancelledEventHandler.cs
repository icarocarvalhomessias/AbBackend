using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
{
    public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} cancelled at {notification.Timestamp}");
        return Task.CompletedTask;
    }
}
