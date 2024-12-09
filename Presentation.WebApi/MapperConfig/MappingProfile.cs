using Application.DTOs.Auth;
using Application.DTOs.Cart;
using Application.DTOs.Order;
using Application.DTOs.Product;
using AutoMapper;
using Ecommerce.Domain.Entities;

namespace Presentation.WebApi.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<Order, OrderDTO>();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<ShoppingCart, CartDTO>();

            CreateMap<CartItem, ViewCartItemDTO>();

            CreateMap<CartDTO, Order>();

            CreateMap<ViewCartItemDTO, OrderItem>();

            CreateMap<ProductUpdateDto, Product>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                {
                    return srcMember != null;
                }));
        }
    }
}
