using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem
{
    public class EditCartProfile : Profile
    {
        public EditCartProfile()
        {
            CreateMap<EditCartRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleCommand, EditCartResponse>();
        }
    }
}
