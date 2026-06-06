using AutoMapper;
using Valsy.Application.Requests;
using Valsy.Domain.Products;

namespace Valsy.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, UpsertProductRequest>().ReverseMap();
        }
    }
}
