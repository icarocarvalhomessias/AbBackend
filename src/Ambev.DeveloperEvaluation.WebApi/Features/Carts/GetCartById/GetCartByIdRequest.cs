using Ambev.DeveloperEvaluation.Common.Validation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById
{
    public class GetCartByIdRequest
    {
        public Guid Id { get; set; }

        public GetCartByIdRequest(Guid id)
        {
            Id = id;
        }

        public ValidationResultDetail validationResultDetail()
        {
            var validator = new GetCartByIdValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
