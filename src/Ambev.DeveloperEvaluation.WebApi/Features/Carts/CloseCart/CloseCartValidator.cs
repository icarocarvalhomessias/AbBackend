using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart;

public class CloseCartValidator : AbstractValidator<CloseCartRequest>
{
    public CloseCartValidator()
    {
        RuleFor(o => o.SaleNumber).NotEmpty();
    }
}
