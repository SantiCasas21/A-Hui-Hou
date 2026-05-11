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
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService) => _orderService = orderService;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderResponse>>> Create([FromBody] CreateOrderRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _orderService.CreateOrderAsync(userId, request);
        return result.IsSuccess
            ? Ok(new ApiResponse<OrderResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<OrderResponse>(false, null, result.Error, result.ErrorCode));
    }
}

