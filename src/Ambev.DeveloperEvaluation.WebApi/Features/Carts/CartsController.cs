using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts
{
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
        public async Task<IActionResult> GetCarts([FromQuery] int _page = 1, [FromQuery] int _size = 10, [FromQuery] string _order = "id desc")
        {
            // Validate query parameters
            if (_page < 1) _page = 1;
            if (_size < 1) _size = 10;

            var request = new GetSalesQuery(_page, _size, _order);

            var carts = await _mediator.Send(request);

            // Prepare response
            var response = new
            {
                data = carts,
                totalItems = carts.Count(),
                currentPage = _page,
                totalPages = (int)Math.Ceiling(carts.Count() / (double)_size)
            };

            return Ok(response);
        }
    }
}
