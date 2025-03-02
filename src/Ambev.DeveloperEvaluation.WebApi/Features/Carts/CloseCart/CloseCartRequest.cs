using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart;

public class CloseCartRequest
{
    public Guid SaleNumber { get; set; }

    public CloseCartRequest(Guid saleNumber)
    {
        SaleNumber = saleNumber;
    }

    public ValidationResultDetail Validate()
    {
        var validator = new CloseCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

}
