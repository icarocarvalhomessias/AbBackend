using Ambev.DeveloperEvaluation.Application.Sales.CloseSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddSaleItem;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing sale operations
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of SaleController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves a paginated list of sales
    /// </summary>
    /// <param name="_page">The page number</param>
    /// <param name="_size">The number of items per page</param>
    /// <param name="_order">The ordering string (e.g., "id desc")</param>
    /// <returns>A paginated list of sales</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetSalesResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSales([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id desc")
    {
        if (_page < 1) _page = 1;
        if (_size < 1) _size = 10;

        var request = new GetSalesQuery(_page, _size, _order);
        var carts = await _mediator.Send(request);

        var response = new ApiResponseWithData<IEnumerable<GetSalesResult>>
        {
            Success = true,
            Message = "Carts retrieved successfully",
            Data = carts
        };

        return Ok(response);
    }

    /// <summary>
    /// Retrieves a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale</param>
    /// <returns>The sale details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSalesResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSaleById(string id)
    {
        var request = new GetSaleByIdQuery(Guid.Parse(id));
        var cart = await _mediator.Send(request);

        if (cart == null)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Cart not found"
            });
        }

        return Ok(new ApiResponseWithData<GetSalesResult>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = cart
        });
    }

    /// <summary>
    /// Adds a new sale item
    /// </summary>
    /// <param name="saleItem">The sale item to add</param>
    /// <returns>The created sale item details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddSaleItem([FromBody] AddSaleItemRequest saleItem)
    {
        var command = new CreateSaleCommand
        {
            Branch = saleItem.Branch,
            ProductId = saleItem.ProductId,
            Quantity = saleItem.Quantity,
            CustomerId = GetCurrentUserId()
        };

        var cart = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddSaleItem), new { id = cart.SaleId }, new ApiResponseWithData<CreateSaleResult>
        {
            Success = true,
            Message = "Product add in cart successfully",
            Data = cart
        });
    }

    /// <summary>
    /// Updates an existing sale
    /// </summary>
    /// <param name="id">The unique identifier of the sale to update</param>
    /// <param name="editSaleItemRequest">The sale item update request</param>
    /// <returns>The updated sale details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditSale(Guid id, [FromBody] EditSaleRequest editSaleItemRequest)
    {
        var command = new UpdateSaleCommand(id, editSaleItemRequest.Products);
        var cart = await _mediator.Send(command);

        return Ok(new ApiResponseWithData<UpdateSaleResult>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = cart
        });
    }

    /// <summary>
    /// Deletes a sale by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the sale to delete</param>
    /// <returns>Success response if the sale was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSale(Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        await _mediator.Send(command);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully"
        });
    }

    /// <summary>
    /// Checks out a sale
    /// </summary>
    /// <param name="id">The unique identifier of the sale to check out</param>
    /// <returns>The checked out sale details</returns>
    [HttpPost("{id}/checkout")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckoutSale(Guid id)
    {
        var command = new CloseSaleCommand(id);
        var cart = await _mediator.Send(command);

        return Ok(new ApiResponseWithData<CloseSaleResult>
        {
            Success = true,
            Message = "Cart checked out successfully",
            Data = cart
        });
    }
}
