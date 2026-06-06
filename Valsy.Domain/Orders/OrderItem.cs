using Valsy.Domain.Common;
using Valsy.Domain.Products;

namespace Valsy.Domain.Orders;

public class OrderItem : AggregateRoot<int>
{
    public int OrderId { get; private set; }
    public Order Order { get; private set; } = default!;
    public int ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public int ProductVariantId { get; private set; }
    public ProductVariant ProductVariant { get; private set; } = default!;
    public string ProductName { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    private OrderItem() { }

    internal static OrderItem Create(
        int orderId,
        int productId,
        int productVariantId,
        string productName,
        string size,
        string color,
        decimal unitPrice,
        int quantity,
        string createdBy)
    {
        var item = new OrderItem
        {
            OrderId = orderId,
            ProductId = productId,
            ProductVariantId = productVariantId,
            ProductName = productName,
            Size = size,
            Color = color,
            UnitPrice = unitPrice,
            Quantity = quantity
        };

        return item;
    }

    internal void IncreaseQuantity(int quantityToAdd, string modifiedBy)
    {
        Quantity += quantityToAdd;
    }
}
