using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public partial class SaleModifiedEvent : Event
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }

    public SaleModifiedEvent(Guid saleId, decimal totalAmount)
    {
        SaleId = saleId;
        TotalAmount = totalAmount;
    }
}
