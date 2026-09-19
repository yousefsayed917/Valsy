using AutoMapper;
using MediatR;
using Valsy.Application.Common.Exceptions;
using Valsy.Application.Orders.Dtos;
using Valsy.Domain.Orders.Repository;

namespace Valsy.Application.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FirstOrDefaultAsync(o => o.Id == request.OrderId, [o => o.Items]);

        return order is null ? throw new NotFoundException("Order not found.", []) : _mapper.Map<OrderDto>(order);
    }
}
