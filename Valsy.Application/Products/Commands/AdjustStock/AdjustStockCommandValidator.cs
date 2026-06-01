using FluentValidation;

namespace Valsy.Application.Products.Commands.AdjustStock;

public class AdjustStockCommandValidator : AbstractValidator<AdjustStockCommand>
{
    public AdjustStockCommandValidator()
    {
        RuleFor(x => x.ProductVariantId).NotEmpty();
        RuleFor(x => x.NewStock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);
    }
}
