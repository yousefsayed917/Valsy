using AutoMapper;
using MediatR;
using Valsy.Domain.Products;
using Valsy.Domain.Products.Repository;

namespace Valsy.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = Product.Create(request.UpsertProductRequest.Name, request.UpsertProductRequest.Description, request.UpsertProductRequest.Price);

        foreach (var variantDto in request.UpsertProductRequest.Variants)
        {
            product.AddVariant(
                variantDto.Size,
                variantDto.Color,
                variantDto.Stock,
                variantDto.Image);
        }
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return Unit.Value;
    }
}
