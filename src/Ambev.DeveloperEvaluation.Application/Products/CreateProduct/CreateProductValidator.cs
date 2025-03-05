using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for product creation command.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, must not exceed 100 characters
    /// - Description: Required, must not exceed 500 characters
    /// - Price: Must be greater than zero
    /// </remarks>
    public CreateProductValidator()
    {
        RuleFor(product => product.Title).NotEmpty().MaximumLength(100);
        RuleFor(product => product.Description).NotEmpty().MaximumLength(500);
        RuleFor(product => product.Price).GreaterThan(0);
    }
}
