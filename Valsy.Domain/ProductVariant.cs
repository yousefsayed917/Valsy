namespace Valsy.Domain;

public class ProductVariant : AggregateRoot
{
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public List<OrderItem> OrderItems { get; private set; } = new();

    private ProductVariant() { }

    public static ProductVariant Create(string size, string color, int stock, int productId, string createdBy)
    {
        var variant = new ProductVariant
        {
            Size = size,
            Color = color,
            Stock = stock,
            ProductId = productId
        };

        return variant;
    }

    public void UpdateStock(int stock, string modifiedBy)
    {
        Stock = stock;
    }
}
