using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<object>> Get()
    {
        return Ok(Array.Empty<object>());
    }
}
