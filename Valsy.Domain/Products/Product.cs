using Valsy.Domain.Common;

namespace Valsy.Domain.Products;

public class Product : AggregateRoot<int>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public List<ProductVariant> Variants { get; private set; } = new();
    private Product() { }

    public static Product Create(string name, string description, decimal price)
    {
        return new Product
        {
            Name = name,
            Description = description,
            Price = price
        };
    }
    public void UpdateDetails(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    public void AddVariant(string size, string color, int stock, string image)
    {
        var variant = ProductVariant.Create(size, color, stock, image);
        Variants.Add(variant);
    }
    public void AdjustVariantStock(int variantId, int newStock)
    {
        // 1. البحث عن الـ Variant داخل المنتج
        var variant = Variants.FirstOrDefault(v => v.Id == variantId) ??
            throw new KeyNotFoundException($"Variant with ID {variantId} not found in this product.");
        variant.UpdateStock(newStock);
    }
}

