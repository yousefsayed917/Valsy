using AutoMapper;
using Valsy.Application.Products.Dtos;
using Valsy.Application.Requests;
using Valsy.Domain.Products;

namespace Valsy.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
