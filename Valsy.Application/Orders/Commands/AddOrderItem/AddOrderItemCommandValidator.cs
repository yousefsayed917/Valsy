using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.AddOrderItem;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustAsync(async (orderId, ct) =>
                await dbContext.Orders.AnyAsync(o => o.Id == orderId, ct))
            .WithMessage("Order does not exist.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .MustAsync(async (productId, ct) =>
                await dbContext.Products.AnyAsync(p => p.Id == productId, ct))
            .WithMessage("Product does not exist.");

        RuleFor(x => x.ProductVariantId)
            .NotEmpty()
            .MustAsync(async (variantId, ct) =>
                await dbContext.ProductVariants.AnyAsync(v => v.Id == variantId, ct))
            .WithMessage("Product variant does not exist.");

        RuleFor(x => x.Quantity).GreaterThan(0);
        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);
    }
}
