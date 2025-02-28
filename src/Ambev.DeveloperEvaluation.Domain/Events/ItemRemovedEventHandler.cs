using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class ItemRemovedEventHandler : INotificationHandler<ItemRemovedEvent>
{
    public Task Handle(ItemRemovedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Item {notification.ItemId} removed from sale {notification.SaleId} at {notification.Timestamp}");
        return Task.CompletedTask;
    }
}
