using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.AddOrderItem;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public AddOrderItemCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken)
            ?? throw new InvalidOperationException("Order not found.");

        var variant = await _dbContext.ProductVariants
            .Include(v => v.Product)
            .FirstOrDefaultAsync(v => v.Id == request.ProductVariantId, cancellationToken)
            ?? throw new InvalidOperationException("Variant not found.");

        if (variant.ProductId != request.ProductId)
        {
            throw new InvalidOperationException("Variant does not belong to the specified product.");
        }

        if (variant.Stock < request.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock for this variant.");
        }

        variant.UpdateStock(variant.Stock - request.Quantity, request.RequestedBy);

        order.AddItem(
            request.ProductId,
            request.ProductVariantId,
            variant.Product.Name,
            variant.Size,
            variant.Color,
            variant.Product.Price,
            request.Quantity,
            request.RequestedBy);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return order.Id;
    }
}
