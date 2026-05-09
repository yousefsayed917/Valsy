using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public SubmitOrderCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken)
            ?? throw new InvalidOperationException("Order not found.");

        order.Submit(request.RequestedBy);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
