using Ambev.DeveloperEvaluation.Application.Products.GetProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

public class GetProductsResponse
{
    public IEnumerable<ProductsResult> Products { get; set; } = new List<ProductsResult>();

    public GetProductsResponse(IEnumerable<ProductsResult?> products)
    {
        Products = products.Any() ? products : new List<ProductsResult>();
    }
}

public class ProductsResult
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Category { get; set; }
}
