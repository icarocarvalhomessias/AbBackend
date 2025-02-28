using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public partial class ItemRemovedEvent : Event
{
    public Guid SaleId { get; set; }
    public Guid ItemId { get; set; }

    public ItemRemovedEvent(Guid saleId, Guid itemId)
    {
        SaleId = saleId;
        ItemId = itemId;
    }
}
