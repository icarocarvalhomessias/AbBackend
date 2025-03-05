using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart
{
    public class DeleteCartProfile : Profile
    {
        public DeleteCartProfile()
        {
            CreateMap<CloseCartRequest, DeleteSaleCommand>();
            CreateMap<DeleteSaleCommand, DeleteCartResponse>();
        }

    }
}
