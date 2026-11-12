using Microsoft.AspNetCore.Mvc;

namespace Eindproject.Controllers;

[ApiController]
[Route("api/info")]
public class InfoController : ControllerBase
{
    [HttpGet]
    public ActionResult<object> Get()
    {
        return Ok(new { message = "hello world" });
    }
}
