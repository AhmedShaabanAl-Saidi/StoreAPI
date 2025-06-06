﻿using AutoMapper;
using Domain.Entities;
using Shared.ProductDtos;

namespace Services.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductResultDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(d => d.PictureUrl, options => options.MapFrom<PictureUrlResolver>());

            CreateMap<ProductType, TypeRessultDto>();
            CreateMap<ProductBrand, BrandResultDto>();
        }
    }
}