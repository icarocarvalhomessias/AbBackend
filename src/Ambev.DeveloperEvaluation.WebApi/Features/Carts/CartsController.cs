using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetSalesResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCarts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id desc")
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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<List<GetSalesResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCartById(int id)
    {
        var request = new GetSalesQuery();
        var cart = await _mediator.Send(request);

        if (cart == null)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = "Cart not found"
            });
        }

        return Ok(new ApiResponseWithData<List<GetSalesResult>>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = cart
        });
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCartItem([FromBody] AddCartItemRequest cartItem)
    {
        var command = new CreateSaleCommand
        {
            Branch = cartItem.Branch,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
            
        };

        var cart = await _mediator.Send(command);
        return CreatedAtAction(nameof(AddCartItem), new { id = cart.SaleId }, new ApiResponseWithData<CreateSaleResult>
        {
            Success = true,
            Message = "Product add in cart successfully",
            Data = cart
        });
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCart(Guid id, [FromBody] Guid productId, [FromBody] int quantity)
    {
        var command = new UpdateSaleCommand(id, productId, quantity);
        var cart = await _mediator.Send(command);

        return Ok(new ApiResponseWithData<UpdateSaleResult>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = cart
        });
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart(Guid id)
    {
        var command = new DeleteSaleCommand { Id = id };
        await _mediator.Send(command);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully"
        });
    }
}
