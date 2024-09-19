using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sulpak.TestAssigment.Application.UseCases;
using Sulpak.TestAssigment.Domain.Entities;

namespace Sulpak.TestAssigment.PublicApi.Controllers.v1;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
public class PriceController : ControllerBase
{
    private readonly PriceUseCase _priceUseCase;

    public PriceController(PriceUseCase priceUseCase)
    {
        _priceUseCase = priceUseCase;
    }

    [HttpGet("{sku}/{department}")]
    public async Task<IActionResult> GetPrice(string sku, string department)
    {
        var price =await _priceUseCase.GetPriceBySkuAndDepartment(sku, department);
        if (price == null)
            return NotFound();
        return Ok(price);
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdatePrices([FromBody] List<Price> prices, int priority)
    {
        await _priceUseCase.UpdatePrices(prices, priority);
        return Ok();
    }
}
