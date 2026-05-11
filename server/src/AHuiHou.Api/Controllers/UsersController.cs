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
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> GetProfile()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.GetProfileAsync(userId);
        return result.IsSuccess
            ? Ok(new ApiResponse<UserResponse>(true, result.Value, null, null))
            : NotFound(new ApiResponse<UserResponse>(false, null, result.Error, result.ErrorCode));
    }

    [HttpPost("membership")]
    public async Task<ActionResult<ApiResponse<UserResponse>>> AssignMembership([FromBody] CreateMembershipRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.AssignMembershipAsync(userId, request);
        return result.IsSuccess
            ? Ok(new ApiResponse<UserResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<UserResponse>(false, null, result.Error, result.ErrorCode));
    }
}

