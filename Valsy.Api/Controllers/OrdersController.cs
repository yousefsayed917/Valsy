using MediatR;
using Microsoft.AspNetCore.Mvc;
using Valsy.Application.Orders.Commands.AddOrderItem;
using Valsy.Application.Orders.Commands.CreateOrder;
using Valsy.Application.Orders.Commands.SubmitOrder;
using Valsy.Application.Orders.Queries.GetOrderById;

namespace Valsy.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var orderId = await _sender.Send(
            new CreateOrderCommand(
                request.CustomerId,
                request.ShippingAddressLine1,
                request.ShippingCity,
                request.ShippingCountry,
                request.ContactPhone,
                request.RequestedBy),
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = orderId }, new { orderId });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var order = await _sender.Send(new GetOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(
            new AddOrderItemCommand(id, request.ProductId, request.ProductVariantId, request.Quantity, request.RequestedBy),
            cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/submit")]
    public async Task<IActionResult> Submit(Guid id, [FromBody] SubmitOrderRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(new SubmitOrderCommand(id, request.RequestedBy), cancellationToken);
        return NoContent();
    }

    public record CreateOrderRequest(
        Guid CustomerId,
        string ShippingAddressLine1,
        string ShippingCity,
        string ShippingCountry,
        string ContactPhone,
        string RequestedBy);

    public record AddOrderItemRequest(
        Guid ProductId,
        Guid ProductVariantId,
        int Quantity,
        string RequestedBy);

    public record SubmitOrderRequest(string RequestedBy);
}
