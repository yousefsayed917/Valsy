using MediatR;
using Valsy.Application.Products.Dtos;

namespace Valsy.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDto?>;
