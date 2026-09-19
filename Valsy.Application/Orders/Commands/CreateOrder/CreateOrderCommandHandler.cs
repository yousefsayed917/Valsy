using MediatR;
using Valsy.Application.Common.Exceptions;
using Valsy.Domain.Customers.Repository;
using Valsy.Domain.Orders.Repository;
using Valsy.Domain.Products.Repository;
namespace Valsy.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(
        ICustomerRepository customerRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetAsyncOrDefault(request.CustomerId);

        if (customer is null)
            throw new NotFoundException("Customer not found.", []);

        var shippingAddress = new Domain.Customers.Address(request.ShippingAddressLine1, request.ShippingCity, request.ShippingCountry);
        var order = Order.Create(request.CustomerId, shippingAddress, request.ContactPhone);

        if (request.Items != null)
        {
            foreach (var item in request.Items)
            {
                var productVariant = await _productRepository.GetAsyncOrDefault(item.ProductVariantId);

                if (productVariant is null)
                    throw new NotFoundException($"Product variant {item.ProductVariantId} not found.", []);

                order.AddItem(productVariant.Variants[0], productVariant.Name, item.Quantity, productVariant.Price, request.RequestedBy);
            }
        }
        await _orderRepository.AddAsync(order);
        await _orderRepository.SaveChangesAsync();

        return order.Id;
    }
}
