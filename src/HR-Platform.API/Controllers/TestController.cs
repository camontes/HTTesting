using HR_Platform.API.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR_Platform.API.Controllers;

[Route("Test")]
public class Tests : ApiController
{

    public Tests()
    {
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Test()
    {
        return Ok(new { message = "Prueba exitosa", obj = true });
    }
}