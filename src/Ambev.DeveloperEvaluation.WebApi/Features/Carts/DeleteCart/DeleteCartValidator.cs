using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;

public class DeleteCartValidator : AbstractValidator<DeleteCartRequest>
{
    public DeleteCartValidator()
    {
        RuleFor(o => o.Id).NotEmpty();
    }
}
