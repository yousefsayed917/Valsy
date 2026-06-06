using MediatR;

namespace Valsy.Application.Orders.Commands.SubmitOrder;

public record SubmitOrderCommand(int OrderId, string RequestedBy) : IRequest;
