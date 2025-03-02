using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.EditCartItem
{
    public class EditCartItemProfile : Profile
    {
        public EditCartItemProfile()
        {
            CreateMap<EditCartItemRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleCommand, EditCartItemResponse>();
        }
    }
}
