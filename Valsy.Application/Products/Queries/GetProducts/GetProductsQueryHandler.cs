using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Products.Repository;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = request.SearchTerm?.ToLower();
        var products = await _productRepository.GetAllIncludingListAsync(
            p => string.IsNullOrWhiteSpace(searchTerm) || p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm),
            [p => p.Variants]
        );

        return _mapper.Map<List<ProductDto>>(products);
    }
}
