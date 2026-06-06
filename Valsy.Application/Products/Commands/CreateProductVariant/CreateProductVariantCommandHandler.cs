using MediatR;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain.Products;

namespace Valsy.Application.Products.Commands.CreateProductVariant;

public class CreateProductVariantCommandHandler : IRequestHandler<CreateProductVariantCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductVariantCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = ProductVariant.Create(
            request.Size,
            request.Color,
            request.Stock,
            request.ProductId,
            request.RequestedBy);

        await _dbContext.ProductVariants.AddAsync(variant, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return variant.Id;
    }
}
