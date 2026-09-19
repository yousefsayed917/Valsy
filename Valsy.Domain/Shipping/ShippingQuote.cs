namespace Valsy.Domain.Shipping;

public record ShippingQuote(decimal Cost, string Currency, string Provider, string ServiceName, DateTime ExpiresAt);