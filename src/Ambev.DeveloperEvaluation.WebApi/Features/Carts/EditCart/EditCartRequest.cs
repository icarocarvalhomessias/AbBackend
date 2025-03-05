
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem;

public class EditCartRequest 
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public EditCartRequest(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new EditCartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }


}
