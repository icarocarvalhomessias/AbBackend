using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
{
    public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} modified to Total Amount: {notification.TotalAmount} at {notification.Timestamp} ");
        return Task.CompletedTask;
    }
}
