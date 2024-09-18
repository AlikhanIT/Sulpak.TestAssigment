using Microsoft.AspNetCore.Mvc;

namespace Sulpak.TestAssigment.PublicApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Get()
    {
        return Task.FromResult<IActionResult>(Ok("test"));
    }
}