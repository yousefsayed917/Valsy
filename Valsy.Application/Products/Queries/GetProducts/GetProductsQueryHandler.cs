using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Domain.Products.Repository;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductCatalogDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductCatalogDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var searchTerm = request.SearchTerm?.ToLower();
        
        // 1. Fetch products matching search term (or all products)
        var allProducts = await _productRepository.GetAllIncludingListAsync(
            p => string.IsNullOrWhiteSpace(searchTerm) || p.Name.ToLower().Contains(searchTerm) || p.Description.ToLower().Contains(searchTerm),
            [p => p.Variants]
        );

        // 2. Compute available filters from this initial result set
        var sizes = allProducts.SelectMany(p => p.Variants).Select(v => v.Size).Distinct().Where(s => !string.IsNullOrEmpty(s)).OrderBy(s => s).ToList();
        var colors = allProducts.SelectMany(p => p.Variants).Select(v => v.Color).Distinct().Where(c => !string.IsNullOrEmpty(c)).OrderBy(c => c).ToList();
        var minPrice = allProducts.Any() ? allProducts.Min(p => p.Price) : 0;
        var maxPrice = allProducts.Any() ? allProducts.Max(p => p.Price) : 0;

        // 3. Apply the specific size, color, and price filters in memory
        var filteredProducts = allProducts.Where(p => 
            (!request.MinPrice.HasValue || p.Price >= request.MinPrice.Value) &&
            (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice.Value) &&
            (string.IsNullOrWhiteSpace(request.Size) || p.Variants.Any(v => v.Size == request.Size)) &&
            (string.IsNullOrWhiteSpace(request.Color) || p.Variants.Any(v => v.Color == request.Color))
        ).ToList();

        return new ProductCatalogDto
        {
            Products = _mapper.Map<List<ProductDto>>(filteredProducts),
            Filters = new ProductFiltersDto
            {
                Sizes = sizes,
                Colors = colors,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            }
        };
    }
}
