using Valsy.Domain.Common;
using Valsy.Domain.Customers;

namespace Valsy.Domain.Orders;

public class Order : AggregateRoot<int>
{
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; } = default!;
    public OrderStatus Status { get; private set; }
    public Address Address { get; private set; }
    public string ContactPhone { get; private set; } = string.Empty;
    public List<OrderItem> Items { get; private set; } = new();
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);

    private Order() { }

    public static Order Create(
        int customerId,
        string shippingAddressLine1,
        string shippingCity,
        string shippingCountry,
        string contactPhone,
        string createdBy)
    {
        var order = new Order
        {
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            Address = new Address(shippingAddressLine1, shippingCity, shippingCountry),
            ContactPhone = contactPhone
        };

        return order;
    }

    public void AddItem(
        int productId,
        int productVariantId,
        string productName,
        string size,
        string color,
        decimal unitPrice,
        int quantity,
        string modifiedBy)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
        }

        if (unitPrice <= 0)
        {
            throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));
        }

        var existingItem = Items.FirstOrDefault(i => i.ProductVariantId == productVariantId);

        if (existingItem is null)
        {
            var item = OrderItem.Create(
                Id,
                productId,
                productVariantId,
                productName,
                size,
                color,
                unitPrice,
                quantity,
                modifiedBy);

            Items.Add(item);
        }
        else
        {
            existingItem.IncreaseQuantity(quantity, modifiedBy);
        }

    }

    public void Submit(string modifiedBy)
    {
        if (Items.Count == 0)
        {
            throw new InvalidOperationException("Cannot submit an order with no items.");
        }

        Status = OrderStatus.Paid;
    }

    public void MarkAsShipped(string modifiedBy)
    {
        if (Status != OrderStatus.Paid)
        {
            throw new InvalidOperationException("Only paid orders can be shipped.");
        }

        Status = OrderStatus.Shipped;
    }

    public void MarkAsDelivered(string modifiedBy)
    {
        if (Status != OrderStatus.Shipped)
        {
            throw new InvalidOperationException("Only shipped orders can be delivered.");
        }

        Status = OrderStatus.Delivered;
    }

    public void Cancel(string modifiedBy)
    {
        if (Status is OrderStatus.Delivered or OrderStatus.Cancelled)
        {
            throw new InvalidOperationException("This order cannot be cancelled.");
        }

        Status = OrderStatus.Cancelled;
    }
}
