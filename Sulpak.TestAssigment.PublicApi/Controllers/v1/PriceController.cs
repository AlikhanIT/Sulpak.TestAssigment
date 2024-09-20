using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Sulpak.TestAssigment.Application.UseCases;
using Sulpak.TestAssigment.Domain.Entities;
using Sulpak.TestAssigment.SharedKernel.Seeders;

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

    [HttpGet("{article}/{departmentId}")]
    public async Task<IActionResult> GetPrice(int article, int departmentId)
    {
        var price = await _priceUseCase.GetPriceByArticleAndDepartmentAsync(article, departmentId);
        if (price is null)
            return NotFound();
        return Ok(price);
    }
    
    [HttpGet("by-department/{departmentId}/priority/{priority}")]
    public async Task<IActionResult> GetPricesByDepartmentAndPriority(int departmentId, int priority)
    {
        var prices = await _priceUseCase.GetPricesByDepartmentAndPriorityAsync(departmentId, priority);
        if (prices == null || !prices.Any())
            return NotFound();
        return Ok(prices);
    }

    /// <summary>
    /// Обновление цен с определенным приоритетом
    /// </summary>
    /// <param name="prices"></param>
    /// <param name="priority"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdatePrices([FromBody] List<Price>? prices, int priority)
    {
        await _priceUseCase.UpdatePricesAsync(prices, priority);
        return Ok();
    }
    
    /// <summary>
    /// Цены постранично
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("paged")]
    public async Task<IActionResult> GetPricesPaged(int pageNumber = 1, int pageSize = 10)
    {
        var (prices, totalRecords) = await _priceUseCase.GetPricesPagedAsync(pageNumber, pageSize);

        return Ok(new 
        {
            TotalRecords = totalRecords,
            Prices = prices
        });
    }
}
