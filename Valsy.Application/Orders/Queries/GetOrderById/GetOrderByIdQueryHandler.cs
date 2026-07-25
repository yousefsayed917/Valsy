using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Orders.Repository;
using Valsy.Application.Orders.Dtos;

namespace Valsy.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FirstOrDefaultAsync(
            o => o.Id == request.OrderId,
            new List<System.Linq.Expressions.Expression<System.Func<Valsy.Domain.Orders.Order, object>>> { o => o.Items }
        );

        return order is null ? null : _mapper.Map<OrderDto>(order);
    }
}
