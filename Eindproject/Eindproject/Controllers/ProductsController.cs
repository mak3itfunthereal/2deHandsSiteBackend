using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<object>> Get()
    {
        return Ok(Array.Empty<object>());
    }
}
