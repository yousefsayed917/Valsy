namespace Valsy.Application.Products.Dtos;

public class ProductFiltersDto
{
    public List<string> Sizes { get; set; } = new();
    public List<string> Colors { get; set; } = new();
    public decimal MinPrice { get; set; }
    public decimal MaxPrice { get; set; }
}
