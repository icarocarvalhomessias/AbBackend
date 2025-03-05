using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById;

public class GetCartByIdValidator : AbstractValidator<GetCartByIdRequest>
{
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
