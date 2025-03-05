using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem;

public class EditCartValidator : AbstractValidator<EditCartRequest>
{
    public EditCartValidator()
    {
        RuleFor(o => o.ProductId).NotEmpty();
        RuleFor(o => o.Quantity).GreaterThan(0);
    }
}
