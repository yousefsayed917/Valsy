using System.Linq.Expressions;
using MediatR;
using Valsy.Domain.Orders;
using Valsy.Domain.Orders.Repository;
using Valsy.Domain.Products;
using Valsy.Domain.Products.Repository;

namespace Valsy.Application.Orders.Commands.AddOrderItem;

public class AddOrderItemCommandHandler : IRequestHandler<AddOrderItemCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<int> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FirstOrDefaultAsync(
            o => o.Id == request.OrderId,
            new List<Expression<Func<Order, object>>> { o => o.Items }
        ) ?? throw new InvalidOperationException("Order not found.");

        var product = await _productRepository.GetAsync(request.ProductId)
            ?? throw new InvalidOperationException("Product not found.");

        var variant = product.Variants.FirstOrDefault(v => v.Id == request.ProductVariantId)
            ?? throw new InvalidOperationException("Variant not found.");

        if (variant.Stock < request.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock for this variant.");
        }

        variant.UpdateStock(variant.Stock - request.Quantity);

        order.AddItem(
            request.ProductId,
            request.ProductVariantId,
            product.Name,
            variant.Size,
            variant.Color,
            product.Price,
            request.Quantity,
            request.RequestedBy);

        await _orderRepository.SaveChangesAsync();
        return order.Id;
    }
}


