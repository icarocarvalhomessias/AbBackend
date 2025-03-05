using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;

public class AddCartItemProfile : Profile
{
    public AddCartItemProfile()
    {
        CreateMap<AddCartItemRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleCommand, AddCartItemResponse>();
    }
}
