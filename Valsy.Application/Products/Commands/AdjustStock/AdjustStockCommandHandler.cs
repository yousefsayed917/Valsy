using MediatR;
using Valsy.Domain.Products.Repository;

namespace Valsy.Application.Products.Commands.AdjustStock;

public class AdjustStockCommandHandler : IRequestHandler<AdjustStockCommand>
{
    private readonly IProductRepository _productRepository;

    public AdjustStockCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(AdjustStockCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAsync(request.productId)
            ?? throw new KeyNotFoundException($"Product with ID '{request.productId}' not found.");

        product.AdjustVariantStock(request.AdjustStockRequest.ProductVariantId, request.AdjustStockRequest.NewStock);

        await _productRepository.SaveChangesAsync();
    }
}