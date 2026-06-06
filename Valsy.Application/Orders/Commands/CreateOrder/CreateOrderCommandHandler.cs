using MediatR;
using Valsy.Application.Common.Interfaces;
using Valsy.Domain;

namespace Valsy.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateOrderCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Create(
            request.CustomerId,
            request.ShippingAddressLine1,
            request.ShippingCity,
            request.ShippingCountry,
            request.ContactPhone,
            request.RequestedBy);

        await _dbContext.Orders.AddAsync(order, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
