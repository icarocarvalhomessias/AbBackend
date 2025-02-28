using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand that defines validation rules for product update command.
/// </summary>
public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - Title: Required, must not exceed 100 characters
    /// - Description: Required, must not exceed 500 characters
    /// - Price: Must be greater than zero
    /// </remarks>
    public UpdateProductValidator()
    {
        RuleFor(product => product.Id).NotEmpty();
        RuleFor(product => product.Title).NotEmpty().MaximumLength(100);
        RuleFor(product => product.Description).NotEmpty().MaximumLength(500);
        RuleFor(product => product.Price).GreaterThan(0);
    }
}
