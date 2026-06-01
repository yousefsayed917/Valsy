using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Products
            .Include(p => p.Variants)
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(searchTerm) || 
                                     p.Description.ToLower().Contains(searchTerm));
        }

        var products = await query.ToListAsync(cancellationToken);
        return _mapper.Map<List<ProductDto>>(products);
    }
}
