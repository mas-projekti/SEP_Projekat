using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Service.Contract.Dto;

namespace WebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, ProductDto>()
               .ForMember(mem => mem.CategoryType, op => op.MapFrom(o => o.CategoryType))
               .ReverseMap();
               

        }
    }
}
