namespace Valsy.Domain;

public class OrderItem : AggregateRoot
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; } = default!;
    public Guid ProductVariantId { get; private set; }
    public ProductVariant ProductVariant { get; private set; } = default!;
    public string ProductName { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    private OrderItem() { }

    internal static OrderItem Create(
        Guid orderId,
        Guid productId,
        Guid productVariantId,
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

        item.MarkCreated(Guid.NewGuid(), createdBy);
        return item;
    }

    internal void IncreaseQuantity(int quantityToAdd, string modifiedBy)
    {
        Quantity += quantityToAdd;
        MarkModified(modifiedBy);
    }
}
