using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.SubmitOrder;

public class SubmitOrderCommandValidator : AbstractValidator<SubmitOrderCommand>
{
    public SubmitOrderCommandValidator()
    {
        

        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);
    }
}
