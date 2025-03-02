using Ambev.DeveloperEvaluation.Application.Sales.CloseSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CloseCart;

public class CloseCartProfile : Profile
{
    public CloseCartProfile()
    {
        CreateMap<CloseCartRequest, CloseSaleCommand>();
        CreateMap<CloseSaleCommand, CloseCartResponse>();
    }
}
