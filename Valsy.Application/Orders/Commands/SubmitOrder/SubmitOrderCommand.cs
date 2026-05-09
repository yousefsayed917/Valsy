using MediatR;

namespace Valsy.Application.Orders.Commands.SubmitOrder;

public record SubmitOrderCommand(Guid OrderId, string RequestedBy) : IRequest;
