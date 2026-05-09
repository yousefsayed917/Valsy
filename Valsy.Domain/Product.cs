namespace Valsy.Domain;

public class Product : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public List<ProductVariant> Variants { get; private set; } = new();
    public List<OrderItem> OrderItems { get; private set; } = new();

    private Product() { }

    public static Product Create(string name, string description, decimal price, string createdBy)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price
        };

        product.MarkCreated(Guid.NewGuid(), createdBy);
        return product;
    }

    public void UpdateDetails(string name, string description, decimal price, string modifiedBy)
    {
        Name = name;
        Description = description;
        Price = price;
        MarkModified(modifiedBy);
    }
}
