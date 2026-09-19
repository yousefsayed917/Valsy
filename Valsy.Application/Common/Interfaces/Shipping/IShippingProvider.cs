using Valsy.Application.Shipping;
using Valsy.Domain.Shipping;

namespace Valsy.Application.Common.Interfaces.Shipping;

public interface IShippingProvider
{
    Task<ShippingQuote> GetQuoteAsync(ShippingRequest request, CancellationToken cancellationToken = default);

    Task<ShipmentResult> CreateShipmentAsync(ShippingRequest request, CancellationToken cancellationToken = default);

    Task<ShipmentStatus> GetShipmentStatusAsync(string trackingNumber, CancellationToken cancellationToken = default);

    Task CancelShipmentAsync(string trackingNumber, CancellationToken cancellationToken = default);
}