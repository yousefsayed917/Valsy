using MediatR;
using Valsy.Application.Requests;

namespace Valsy.Application.Products.Commands.AdjustStock;

public record AdjustStockCommand(int productId, AdjustStockRequest AdjustStockRequest) : IRequest;
