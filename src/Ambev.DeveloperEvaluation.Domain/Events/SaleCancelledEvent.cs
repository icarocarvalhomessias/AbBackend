using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public partial class SaleCancelledEvent : Event
{
    public Guid SaleId { get; set; }

    public SaleCancelledEvent(Guid saleId)
    {
        SaleId = saleId;
    }
}
