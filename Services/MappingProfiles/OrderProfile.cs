using AutoMapper;
using Domain.Entities.Identity;
using Shared.IdentityDtos;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile() 
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }
    }
}
