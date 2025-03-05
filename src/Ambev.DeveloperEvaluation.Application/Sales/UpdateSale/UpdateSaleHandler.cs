using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mediator = mediator;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = request.validationResultDetail();
        if (validationResult == null)
            throw new InvalidOperationException(string.Join(", ", validationResult.Errors));

        var sale = await _saleRepository.GetByIdAsync(request.SaleId, cancellationToken);

        if (sale == null)
        {
            throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found");
        }

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) {
            throw new InvalidOperationException("Product not found.");
        }

        var saleItem = sale.Items.FirstOrDefault(si => si.ProductId == request.ProductId);

        if (saleItem == null)
        {
            sale.AddItem(new SaleItem(product, request.Quantity, sale.Id));
        }
        else
        {
            saleItem.ChangeQuantity(request.Quantity);

            if (saleItem.IsCancelled)
            {
                sale.RemoveItem(saleItem);
                await _mediator.Publish(new ItemRemovedEvent(sale.Id, saleItem.Id), cancellationToken);
            }
        }

        sale.CalculateSale();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _mediator.Publish(new SaleModifiedEvent(sale.Id, sale.TotalAmount), cancellationToken);

        return new UpdateSaleResult
        {
            TotalAmount = sale.TotalAmount,
            SaleId = sale.Id
        };
    }
}
