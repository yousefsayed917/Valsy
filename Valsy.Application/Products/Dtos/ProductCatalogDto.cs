namespace Valsy.Application.Products.Dtos;

public class ProductCatalogDto
{
    public List<ProductDto> Products { get; set; } = new();
    public ProductFiltersDto Filters { get; set; } = new();
}
