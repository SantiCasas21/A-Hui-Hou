using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromotionsController : ControllerBase
{
    private readonly IPromotionService _promotionService;

    public PromotionsController(IPromotionService promotionService) => _promotionService = promotionService;

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<PromotionResponse>>>> GetActive()
    {
        var result = await _promotionService.GetActiveAsync();
        return Ok(new ApiResponse<IEnumerable<PromotionResponse>>(true, result.Value, null, null));
    }
}
