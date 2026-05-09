using MediatR;

namespace Valsy.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    decimal Price,
    string RequestedBy) : IRequest<Guid>;
