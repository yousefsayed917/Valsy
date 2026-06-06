using AutoMapper;
using Valsy.Application.Products.Dtos;
using Valsy.Domain.Products;

namespace Valsy.Application.Products.Mapping;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<ProductVariant, ProductVariantDto>();
    }
}
