using MediatR;

namespace Valsy.Application.Orders.Commands.AddOrderItem;

public record AddOrderItemCommand(
    int OrderId,
    int ProductId,
    int ProductVariantId,
    int Quantity,
    string RequestedBy) : IRequest<int>;
