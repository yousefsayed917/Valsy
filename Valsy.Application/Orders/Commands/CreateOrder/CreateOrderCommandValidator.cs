using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Valsy.Application.Common.Interfaces;

namespace Valsy.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        

        RuleFor(x => x.ShippingAddressLine1).NotEmpty().MaximumLength(250);
        RuleFor(x => x.ShippingCity).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ShippingCountry).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ContactPhone).NotEmpty().MaximumLength(30);
        RuleFor(x => x.RequestedBy).NotEmpty().MaximumLength(100);
    }
}
