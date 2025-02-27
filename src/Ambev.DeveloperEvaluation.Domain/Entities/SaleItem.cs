using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid SaleId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPrice { get; set; } = 0;
        public decimal TotalAmount { get; set; } = 0;
        public bool IsCancelled { get; set; } = false;

        public Sale Sale { get; set; }
        public Product Product { get; set; }

        // EF Constructor
        protected SaleItem() { }

        public SaleItem(Product product, int quantity, Guid saleId)
        {
            ProductId = product.Id;
            Quantity = quantity;
            UnitPrice = product.Price;
            SaleId = saleId;
            CalculateTotalAmount();
        }

        public void CalculateTotalAmount()
        {
            TotalAmount = (UnitPrice * Quantity);
        }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
            CalculateTotalAmount();
        }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
