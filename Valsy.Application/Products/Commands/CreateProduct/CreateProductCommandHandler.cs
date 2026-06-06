using AutoMapper;
using MediatR;
using Valsy.Domain.Products;
using Valsy.Domain.Products.Repository;

namespace Valsy.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = _mapper.Map<Product>(request);
        product.Create(product);

        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();

        return product.Id;
    }
}
