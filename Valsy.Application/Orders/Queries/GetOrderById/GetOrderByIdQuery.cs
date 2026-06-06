using MediatR;
using Valsy.Application.Orders.Dtos;

namespace Valsy.Application.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(int OrderId) : IRequest<OrderDto?>;
