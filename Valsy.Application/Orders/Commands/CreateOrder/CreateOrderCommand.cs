using System.Collections.Generic;
using MediatR;

namespace Valsy.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    int CustomerId,
    string ShippingAddressLine1,
    string ShippingCity,
    string ShippingCountry,
    string ContactPhone,
    string RequestedBy,
    List<OrderItemCommand> Items) : IRequest<int>;

public record OrderItemCommand(int ProductVariantId, int Quantity);
