using Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartById
{
    public class GetCartByIdProfile : Profile
    {
        public GetCartByIdProfile()
        {
            CreateMap<GetCartByIdRequest, GetSaleByIdHandler>();
            CreateMap<GetSaleByIdHandler, GetSalesResponse>();
        }
    }
}
