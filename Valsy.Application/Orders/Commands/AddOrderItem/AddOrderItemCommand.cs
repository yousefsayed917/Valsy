using MediatR;

namespace Valsy.Application.Orders.Commands.AddOrderItem;

public record AddOrderItemCommand(
    Guid OrderId,
    Guid ProductId,
    Guid ProductVariantId,
    int Quantity,
    string RequestedBy) : IRequest<Guid>;
