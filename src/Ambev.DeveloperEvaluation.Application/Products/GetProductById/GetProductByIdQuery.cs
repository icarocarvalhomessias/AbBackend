using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdResults>
{
    public Guid Id { get; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}
