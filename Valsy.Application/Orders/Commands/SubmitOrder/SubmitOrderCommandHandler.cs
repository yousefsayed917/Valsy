using MediatR;
using Valsy.Domain.Orders.Repository;
namespace Valsy.Application.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand>
{
    private readonly IOrderRepository _orderRepository;

    public SubmitOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FirstOrDefaultAsync(
            o => o.Id == request.OrderId, [o => o.Items]) ?? throw new InvalidOperationException("Order not found.");

        order.Submit();
        await _orderRepository.SaveChangesAsync();
    }
}
