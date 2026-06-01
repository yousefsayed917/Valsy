using MediatR;

namespace Valsy.Application.Products.Commands.AdjustStock;

public record AdjustStockCommand(
    Guid ProductVariantId,
    int NewStock,
    string RequestedBy) : IRequest;
