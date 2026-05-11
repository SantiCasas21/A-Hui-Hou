using System.Security.Claims;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoyaltyController : ControllerBase
{
    private readonly ILoyaltyService _loyaltyService;

    public LoyaltyController(ILoyaltyService loyaltyService) => _loyaltyService = loyaltyService;

    [HttpGet("balance")]
    public async Task<ActionResult<ApiResponse<LoyaltyWalletResponse>>> GetBalance()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _loyaltyService.GetBalanceAsync(userId);
        return Ok(new ApiResponse<LoyaltyWalletResponse>(true, result.Value, null, null));
    }

    [HttpPost("redeem")]
    public async Task<ActionResult<ApiResponse<LoyaltyWalletResponse>>> Redeem([FromBody] RedeemPointsRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _loyaltyService.RedeemPointsAsync(userId, request);
        return result.IsSuccess
            ? Ok(new ApiResponse<LoyaltyWalletResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<LoyaltyWalletResponse>(false, null, result.Error, result.ErrorCode));
    }

    [HttpGet("transactions")]
    public async Task<ActionResult<ApiResponse<IEnumerable<PointTransactionResponse>>>> GetTransactions()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _loyaltyService.GetTransactionsAsync(userId);
        return Ok(new ApiResponse<IEnumerable<PointTransactionResponse>>(true, result.Value, null, null));
    }
}

