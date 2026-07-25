using MediatR;
using Microsoft.AspNetCore.Mvc;
using Valsy.Application.Products.Commands.AdjustStock;
using Valsy.Application.Products.Commands.CreateProduct;
using Valsy.Application.Products.Queries.GetProducts;
using Valsy.Application.Requests;

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
    public async Task<IActionResult> Create([FromBody] UpsertProductRequest request, CancellationToken cancellationToken)
    {
        await _sender.Send(new CreateProductCommand(request), cancellationToken);
        return Created();
    }

    [HttpPut("productvariants/{productId:int}/stock")]
    public async Task<IActionResult> AdjustStock(
        int productId,
        [FromBody] AdjustStockRequest request,
        CancellationToken cancellationToken)
    {
        await _sender.Send(new AdjustStockCommand(productId, request), cancellationToken);
        return NoContent();
    }
}
