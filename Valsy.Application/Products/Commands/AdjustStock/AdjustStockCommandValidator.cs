using FluentValidation;

namespace Valsy.Application.Products.Commands.AdjustStock;

public class AdjustStockCommandValidator : AbstractValidator<AdjustStockCommand>
{
    public AdjustStockCommandValidator()
    {
        RuleFor(x => x.AdjustStockRequest.ProductVariantId).NotEmpty();
        RuleFor(x => x.AdjustStockRequest.NewStock).GreaterThanOrEqualTo(0);
    }
}
