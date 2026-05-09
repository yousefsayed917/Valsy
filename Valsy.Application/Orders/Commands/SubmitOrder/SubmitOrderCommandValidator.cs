using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
{
    public SubmitOrderCommandValidator(IApplicationDbContext dbContext)
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustAsync(async (orderId, ct) =>
                await dbContext.Orders.AnyAsync(o => o.Id == orderId, ct))
            .WithMessage("Order does not exist.");

        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);
    }
}
