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
    private readonly IProductVariantRepository _productVariantRepository;

    public AddOrderItemCommandHandler(IOrderRepository orderRepository, IProductVariantRepository productVariantRepository)
    {
        _orderRepository = orderRepository;
        _productVariantRepository = productVariantRepository;
    }

    public async Task<int> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FirstOrDefaultAsync(
            o => o.Id == request.OrderId,
            new List<Expression<Func<Order, object>>> { o => o.Items }
        ) ?? throw new InvalidOperationException("Order not found.");

        var variant = await _productVariantRepository.FirstOrDefaultAsync(
            v => v.Id == request.ProductVariantId
        ) ?? throw new InvalidOperationException("Variant not found.");

        if (variant.ProductId != request.ProductId)
        {
            throw new InvalidOperationException("Variant does not belong to the specified product.");
        }

        if (variant.Stock < request.Quantity)
        {
            throw new InvalidOperationException("Insufficient stock for this variant.");
        }

        variant.UpdateStock(variant.Stock - request.Quantity);

        order.AddItem(
            request.ProductId,
            request.ProductVariantId,
            "Product Name", // You may need to fetch this from the product
            variant.Size,
            variant.Color,
            0, // You may need to fetch the price from the product
            request.Quantity,
            request.RequestedBy);

        await _orderRepository.SaveChangesAsync();
        return order.Id;
    }
}


