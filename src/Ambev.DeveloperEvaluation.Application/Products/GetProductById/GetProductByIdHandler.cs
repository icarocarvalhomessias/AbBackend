using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResults>
    {

        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductByIdResults> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");
            }

            return new GetProductByIdResults
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}
