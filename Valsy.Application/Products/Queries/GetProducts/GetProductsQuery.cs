using MediatR;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProducts;

public record GetProductsQuery(
    string SearchTerm = null,
    string Size = null,
    string Color = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null
) : IRequest<ProductCatalogDto>;
