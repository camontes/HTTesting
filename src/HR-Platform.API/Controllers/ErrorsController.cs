using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HR_Platform.API.Controllers;

public class ErrorsController : ControllerBase
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/error")]
    public IActionResult Error()
    {
        _ = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}