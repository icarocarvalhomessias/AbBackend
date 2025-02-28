using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleCreatedEvent : Event
{
    public Guid SaleId { get; set; }

    public SaleCreatedEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
