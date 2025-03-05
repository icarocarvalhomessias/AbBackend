
using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem;

public class EditCartItemRequest 
{
    public int CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public EditCartItemRequest(int cartId, Guid productId, int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new EditCartItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }


}
