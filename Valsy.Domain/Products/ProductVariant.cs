using Valsy.Domain.Common;

public class ProductVariant : Entity<int>
{
    public string ProductVariantCode { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public int Stock { get; private set; }
    public int ProductId { get; private set; }
    public byte[] RowVersion { get; private set; } = [];
    private ProductVariant() { }
    internal static ProductVariant Create(string size, string color, int stock, string image)
    {
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");

        return new ProductVariant
        {
            ProductVariantCode = GenerateProductVariantCode(),
            Size = size,
            Color = color,
            Image = image,
            Stock = stock
        };
    }
    private string GenerateProductVariantCode()
        => $"SP-{this.Size}-{this.Color}";

    public void UpdateStock(int stock)
    {
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");

        Stock = stock;
    }
}