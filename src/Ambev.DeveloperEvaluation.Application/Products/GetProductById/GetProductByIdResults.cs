namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

public class GetProductByIdResults
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; }
}
