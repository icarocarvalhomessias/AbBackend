using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem;

public class EditCartItemValidator : AbstractValidator<EditCartItemRequest>
{
    public EditCartItemValidator()
    {
        RuleFor(o => o.CartId).GreaterThan(0);
        RuleFor(o => o.ProductId).GreaterThan(0);
        RuleFor(o => o.Quantity).GreaterThan(0);
    }
}
