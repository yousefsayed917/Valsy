namespace Valsy.Domain;

public class ProductVariant : AggregateRoot
{
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public Guid ProductId { get; private set; }

    private ProductVariant() { }

    public static ProductVariant Create(string size, string color, int stock, Guid productId, string createdBy)
    {
        var variant = new ProductVariant
        {
            Size = size,
            Color = color,
            Stock = stock,
            ProductId = productId
        };

        variant.MarkCreated(Guid.NewGuid(), createdBy);
        return variant;
    }

    public void UpdateStock(int stock, string modifiedBy)
    {
        Stock = stock;
        MarkModified(modifiedBy);
    }
}
