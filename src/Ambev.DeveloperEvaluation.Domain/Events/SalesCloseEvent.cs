using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SalesCloseEvent : Event
{
    public Guid SaleId { get; set; }
    public decimal TotalAmount { get; set; }
    public string UserEmail { get; set; }

    public SalesCloseEvent(Guid saleId, decimal totalAmount, string userEmail)
    {
        SaleId = saleId;
        TotalAmount = totalAmount;
        UserEmail = userEmail;
    }
}
