using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    public Guid SaleId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }


    public UpdateSaleCommand(Guid saleId, Guid productId, int quantity)
    {
        SaleId = saleId;
        ProductId = productId;
        Quantity = quantity;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new UpdateSaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
