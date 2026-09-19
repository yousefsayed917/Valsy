using Valsy.Domain.Common;
using Valsy.Domain.Common.Enums;
using Valsy.Domain.Customers;
using Valsy.Domain.Orders;

public class Order : AggregateRoot<int>
{
    public int CustomerId { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public OrderStatus Status { get; private set; }
    public Address ShippingAddress { get; private set; } = null!;
    public string ContactPhone { get; private set; } = string.Empty;
    public decimal ItemsAmount { get; private set; }
    public decimal Discount { get; private set; }
    public decimal ShippingCost { get; private set; }
    public decimal TotalAmount { get; private set; }
    public string? PromoCode { get; private set; }

    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order(int customerId, Address shippingAddress, string contactPhone)
    {
        CustomerId = customerId;
        ShippingAddress = shippingAddress;
        ContactPhone = contactPhone;
        Status = OrderStatus.Pending;
    }


    public static Order Create(int customerId, Address shippingAddress, string contactPhone)
    {
        if (customerId <= 0)
            throw new ArgumentException("Customer ID must be greater than zero.", nameof(customerId));

        if (shippingAddress is null)
            throw new ArgumentNullException(nameof(shippingAddress));

        if (string.IsNullOrWhiteSpace(contactPhone))
            throw new ArgumentException("Contact phone is required.", nameof(contactPhone));

        return new Order(customerId, shippingAddress, contactPhone);
    }
    public void AddItem(ProductVariant productVariant, string productName, int quantity, decimal unitPrice, string createdBy)
    {
        if (productVariant is null)
            throw new ArgumentNullException(nameof(productVariant));

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name is required.", nameof(productName));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));

        if (string.IsNullOrWhiteSpace(createdBy))
            throw new ArgumentException("Created by is required.", nameof(createdBy));

        var existingItem = _items.FirstOrDefault(x => x.ProductVariantId == productVariant.Id);

        if (existingItem is not null)
        {
            existingItem.IncreaseQuantity(quantity, createdBy);
        }
        else
        {
            var orderItem = OrderItem.Create(Id, productVariant.Id, productName, productVariant.Size, productVariant.Color, unitPrice, quantity, createdBy);
            _items.Add(orderItem);
        }
    }
    public void ApplyDiscount(string promoCode, decimal discountAmount)
    {
        if (string.IsNullOrWhiteSpace(promoCode))
            throw new ArgumentException("Promo code is required.", nameof(promoCode));

        if (discountAmount < 0)
            throw new ArgumentOutOfRangeException(nameof(discountAmount));

        if (discountAmount > ItemsAmount)
            throw new ArgumentException("Discount cannot be greater than the items amount.", nameof(discountAmount));

        PromoCode = promoCode.Trim().ToUpperInvariant();
        Discount = discountAmount;
        TotalAmount = ItemsAmount - Discount + ShippingCost;
    }
    public void Submit()
        => Status = _items.Count == 0
        ? throw new InvalidOperationException("An order must contain at least one item.") : OrderStatus.Submitted;

    public void MarkAsShipped()
        => Status = Status != OrderStatus.Submitted
        ? throw new InvalidOperationException("Only submitted orders can be shipped.") : OrderStatus.Shipped;

    public void MarkAsDelivered()
        => Status = Status != OrderStatus.Shipped
        ? throw new InvalidOperationException("Only shipped orders can be delivered.") : OrderStatus.Delivered;

    public void Cancel()
        => Status = Status == OrderStatus.Delivered || Status == OrderStatus.Cancelled
        ? throw new InvalidOperationException("This order cannot be cancelled.") : OrderStatus.Cancelled;
}