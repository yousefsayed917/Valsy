using AutoMapper;
using MediatR;
using Valsy.Application.Products.Dtos;
using Valsy.Domain.Products.Repository;

namespace Valsy.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FirstOrDefaultAsync(p => p.Id == request.ProductId);

        return product is null ? null : _mapper.Map<ProductDto>(product);
    }
}
