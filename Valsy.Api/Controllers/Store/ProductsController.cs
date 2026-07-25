using MediatR;
using Microsoft.AspNetCore.Mvc;
using Valsy.Application.Products.Queries.GetProductById;
using Valsy.Application.Products.Queries.GetProducts;

namespace Valsy.Api.Controllers.Store;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ISender _sender;

    public ProductsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetCatalog([FromQuery] string? searchTerm, CancellationToken cancellationToken)
    {
        var products = await _sender.Send(new GetProductsQuery(searchTerm), cancellationToken);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var product = await _sender.Send(new GetProductByIdQuery(id), cancellationToken);
        if (product is null)
        {
            return NotFound(new { message = $"Product with ID '{id}' was not found." });
        }
        return Ok(product);
    }
}
