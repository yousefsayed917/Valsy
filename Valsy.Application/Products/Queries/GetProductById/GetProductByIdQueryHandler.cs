using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .Include(p => p.Variants)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);

        return product is null ? null : _mapper.Map<ProductDto>(product);
    }
}
