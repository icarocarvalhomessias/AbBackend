using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{

    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public Rating Rating { get; set; } = new Rating();
        public bool IsAvailable { get; protected set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public Product()
        {
            CreatedAt = DateTime.Now;
        }

        public void Activate()
        {
            UpdatedAt = DateTime.Now;
            IsAvailable = true;
        }

        public void Deactivate()
        {
            UpdatedAt = DateTime.Now;
            IsAvailable = false;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new ProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

    }

    public class Rating
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}
