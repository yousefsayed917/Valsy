using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Products.Commands.AdjustStock;

public class AdjustStockCommandHandler : IRequestHandler<AdjustStockCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public AdjustStockCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AdjustStockCommand request, CancellationToken cancellationToken)
    {
        var variant = await _dbContext.ProductVariants
            .FirstOrDefaultAsync(v => v.Id == request.ProductVariantId, cancellationToken)
            ?? throw new KeyNotFoundException($"Product variant with ID '{request.ProductVariantId}' not found.");

        variant.UpdateStock(request.NewStock, request.RequestedBy);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
