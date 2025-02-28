using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
{
    public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Sale {notification.SaleId} created at {notification.Timestamp}");
        return Task.CompletedTask;
    }
}
