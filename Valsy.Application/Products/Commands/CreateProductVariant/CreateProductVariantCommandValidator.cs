using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Products.Commands.CreateProductVariant;

public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
{
    public CreateProductVariantCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (productId, ct) =>
                await dbContext.Products.AnyAsync(p => p.Id == productId, ct))
            .WithMessage("Product does not exist.");

        RuleFor(x => x.Size).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Color).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0);
        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);

        RuleFor(x => x)
            .MustAsync(async (request, ct) =>
                !await dbContext.ProductVariants.AnyAsync(
                    v => v.ProductId == request.ProductId
                        && v.Size == request.Size
                        && v.Color == request.Color,
                    ct))
            .WithMessage("This variant already exists for the product.");
    }
}
