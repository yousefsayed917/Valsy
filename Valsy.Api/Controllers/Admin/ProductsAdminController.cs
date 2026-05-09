using Microsoft.AspNetCore.Mvc;

namespace Valsy.Api.Controllers.Admin;

[ApiController]
[Route("api/admin/products")]
public class ProductsAdminController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new { message = "Admin products endpoint is ready." });
    }
}
