using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductsResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProducts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id desc")
        {
            if (_page < 1) _page = 1;
            if (_size < 1) _size = 10;

            var request = new GetProductsQuery(_page, _size, _order);
            var products = await _mediator.Send(request);

            var response = new ApiResponseWithData<GetProductsResult>
            {
                Success = true,
                Message = "Products retrieved successfully",
                Data = products
            };

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] string title, [FromBody] string description, [FromBody] decimal price, [FromBody] string category)
        {
            var command = new CreateProductCommand
            {
                Title = title,
                Description = description,
                Price = price,
                Category = category
            };

            var result = await _mediator.Send(command);
            var response = _mapper.Map<CreateProductResponse>(result);

            return CreatedAtAction(nameof(CreateProduct), new { id = response.Id }, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Product created successfully",
                Data = response
            });
        }
    }
}
