using Valsy.Application.Shipping;
using Valsy.Domain.Common;

public class Shipment : AggregateRoot<int>
{
    public int OrderId { get; private set; }

    public string Provider { get; private set; } = string.Empty;

    public string TrackingNumber { get; private set; } = string.Empty;

    public ShipmentStatus Status { get; private set; }

    public decimal ShippingCost { get; private set; }

    private Shipment() { }
}