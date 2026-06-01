using Microsoft.AspNetCore.Mvc;
using MediatR;
using Valsy.Application.Products.Commands.CreateProduct;
using Valsy.Application.Products.Commands.CreateProductVariant;
using Valsy.Application.Products.Commands.AdjustStock;
using Valsy.Application.Products.Queries.GetProducts;

namespace Valsy.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/products")]
public class ProductsAdminController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsAdminController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? searchTerm, CancellationToken cancellationToken)
    {
        var products = await _sender.Send(new GetProductsQuery(searchTerm), cancellationToken);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var productId = await _sender.Send(
            new CreateProductCommand(request.Name, request.Description, request.Price, request.RequestedBy),
            cancellationToken);

        return Ok(new { productId });
    }

    [HttpPost("{productId:guid}/variants")]
    public async Task<IActionResult> CreateVariant(
        Guid productId,
        [FromBody] CreateProductVariantRequest request,
        CancellationToken cancellationToken)
    {
        var variantId = await _sender.Send(
            new CreateProductVariantCommand(
                productId,
                request.Size,
                request.Color,
                request.Stock,
                request.RequestedBy),
            cancellationToken);

        return Ok(new { variantId });
    }

    [HttpPut("variants/{variantId:guid}/stock")]
    public async Task<IActionResult> AdjustStock(
        Guid variantId,
        [FromBody] AdjustStockRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(new AdjustStockCommand(variantId, request.NewStock, request.RequestedBy), cancellationToken);
        return NoContent();
    }

    public record CreateProductRequest(string Name, string Description, decimal Price, string RequestedBy);
    public record CreateProductVariantRequest(string Size, string Color, int Stock, string RequestedBy);
    public record AdjustStockRequest(int NewStock, string RequestedBy);
}
