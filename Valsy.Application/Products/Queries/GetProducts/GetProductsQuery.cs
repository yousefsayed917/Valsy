using MediatR;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProducts;

public record GetProductsQuery(string? SearchTerm = null) : IRequest<List<ProductDto>>;
