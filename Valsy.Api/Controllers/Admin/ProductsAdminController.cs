using Microsoft.AspNetCore.Mvc;
using MediatR;
using Valsy.Application.Products.Commands.CreateProduct;
using Valsy.Application.Products.Commands.CreateProductVariant;

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
    public IActionResult GetAll()
    {
        return Ok(new { message = "Admin products endpoint is ready." });
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

    public record CreateProductRequest(string Name, string Description, decimal Price, string RequestedBy);
    public record CreateProductVariantRequest(string Size, string Color, int Stock, string RequestedBy);
}
