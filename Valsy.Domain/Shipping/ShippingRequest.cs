using Valsy.Domain.Customers;

namespace Valsy.Domain.Shipping;

public class ShippingRequest
{
    public Address PickupAddress { get; init; } = null!;
    public Address DeliveryAddress { get; init; } = null!;
    public decimal OrderAmount { get; init; }
    public decimal Weight { get; init; }
}