using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}

