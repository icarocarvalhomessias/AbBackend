using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;

public class AddCartItemRequest
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public string Branch { get; set; }

    public AddCartItemRequest(Guid productId, int quantity, string branch)
    {
        ProductId = productId;
        Quantity = quantity;
        Branch = branch;
    }

    public ValidationResultDetail validationResultDetail()
    {
        var validator = new AddCartItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
