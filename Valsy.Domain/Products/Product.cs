using Valsy.Domain.Common;
using Valsy.Domain.Orders;

namespace Valsy.Domain.Products;

public class Product : AggregateRoot<int>
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public List<ProductVariant> Variants { get; private set; } = new();
    public List<OrderItem> OrderItems { get; private set; } = new();

    private Product() { }

    public Product Create(Product product)
    {
        return product;
    }

    public void UpdateDetails(string name, string description, decimal price, string modifiedBy)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
