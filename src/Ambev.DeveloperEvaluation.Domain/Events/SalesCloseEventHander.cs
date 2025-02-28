using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SalesCloseEventHander : INotificationHandler<SalesCloseEvent>
{
    public Task Handle(SalesCloseEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} closed at {notification.Timestamp} with Total Amount: {notification.TotalAmount}");
        return Task.CompletedTask;
    }
}
