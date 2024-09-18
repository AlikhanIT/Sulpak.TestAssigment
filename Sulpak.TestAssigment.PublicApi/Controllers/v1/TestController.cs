using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Sulpak.TestAssigment.PublicApi.Controllers.v1;

/// <summary>
/// test sasasasasasassa
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class TeamController : BaseController
{
    /// <summary>
    /// test sasa
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTeam")]
    public IActionResult GetV1()
    {
        throw new Exception("testes");
        return Ok("V1 Get to be implemented");
    }
}