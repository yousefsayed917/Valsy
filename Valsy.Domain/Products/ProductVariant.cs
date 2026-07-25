using Valsy.Domain.Common;

namespace Valsy.Domain.Products;

public class ProductVariant : Entity<int>
{
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public int ProductId { get; private set; }

    private ProductVariant() { }
    internal static ProductVariant Create(string size, string color, int stock, string image)
    {
        return new ProductVariant
        {
            Size = size,
            Color = color,
            Image = image,
            Stock = stock
        };
    }
    public void UpdateStock(int stock)
    {
        Stock = stock;
    }
}