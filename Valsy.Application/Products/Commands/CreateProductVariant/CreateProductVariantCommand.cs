using MediatR;

namespace Valsy.Application.Products.Commands.CreateProductVariant;

public record CreateProductVariantCommand(
    Guid ProductId,
    string Size,
    string Color,
    int Stock,
    string RequestedBy) : IRequest<Guid>;
