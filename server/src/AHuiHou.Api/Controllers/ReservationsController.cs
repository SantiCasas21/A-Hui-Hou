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
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService) => _reservationService = reservationService;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ReservationResponse>>> Create([FromBody] CreateReservationRequest request)
    {
        var userId = GetUserId();
        var result = await _reservationService.CreateReservationAsync(userId, request);
        return result.IsSuccess
            ? Ok(new ApiResponse<ReservationResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<ReservationResponse>(false, null, result.Error, result.ErrorCode));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ReservationResponse>>>> GetMyReservations()
    {
        var userId = GetUserId();
        var result = await _reservationService.GetUserReservationsAsync(userId);
        return Ok(new ApiResponse<IEnumerable<ReservationResponse>>(true, result.Value, null, null));
    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult<ApiResponse<object>>> Cancel(Guid id)
    {
        var userId = GetUserId();
        var result = await _reservationService.CancelReservationAsync(id, userId);
        return result.IsSuccess
            ? Ok(new ApiResponse<object>(true, null, null, null))
            : BadRequest(new ApiResponse<object>(false, null, result.Error, result.ErrorCode));
    }

    private Guid GetUserId()
        => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}

