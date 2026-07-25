using Valsy.Domain.Orders;
using Valsy.Domain.Products;

namespace Valsy.Application.Requests
{
    public record UpsertProductRequest(
        string Name,
        string Description,
        decimal Price,
        List<VariantDto> Variants);
    
    public record VariantDto(string Size, string Color, int Stock, string Image);
}


