using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Register([FromBody] RegisterUserRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        return result.IsSuccess
            ? Ok(new ApiResponse<AuthResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<AuthResponse>(false, null, result.Error, result.ErrorCode));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponse>>> Login([FromBody] LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return result.IsSuccess
            ? Ok(new ApiResponse<AuthResponse>(true, result.Value, null, null))
            : Unauthorized(new ApiResponse<AuthResponse>(false, null, result.Error, result.ErrorCode));
    }
}

