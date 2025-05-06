using AutoMapper;
using Domain.Entities.OrderEntities;
using Shared.OrderDtos;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Address,AddressDto>().ReverseMap();

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl));

            CreateMap<Order, OrderResult>()
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.PaymentStatus.ToString()))
                .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Subtotal + src.DeliveryMethod.Price));

            CreateMap<DeliveryMethod, DeliveryMethodResult>();
        }
    }
}
