using Microsoft.AspNetCore.Mvc;

namespace Valsy.Api.Controllers.Store;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCatalog()
    {
        return Ok(new { message = "Store products endpoint is ready." });
    }
}
