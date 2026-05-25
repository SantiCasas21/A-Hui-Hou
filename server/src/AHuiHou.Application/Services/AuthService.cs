using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AHuiHou.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        IJwtTokenService jwtTokenService,
        ILogger<AuthService> logger)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
        _logger = logger;
    }

    public async Task<Result<AuthResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
        if (existing is not null)
            return Result<AuthResponse>.Failure("Email already registered", "EMAIL_DUPLICATE");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        var wallet = new LoyaltyWallet
        {
            UserId = user.Id,
            Balance = 0m,
            LastUpdate = DateTime.UtcNow
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.LoyaltyWallets.AddAsync(wallet, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User {Email} registered successfully", user.Email);

        var token = _jwtTokenService.GenerateToken(user.Id, user.Email, "Member");
        return Result<AuthResponse>.Success(new AuthResponse(token, user.Email, user.FirstName, user.LastName));
    }

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return Result<AuthResponse>.Failure("Invalid credentials", "INVALID_CREDENTIALS");

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result<AuthResponse>.Failure("Invalid credentials", "INVALID_CREDENTIALS");

        var hasMembership = user.Memberships.Any(m => m.IsActive);
        var role = hasMembership ? "Member" : "Guest";

        var token = _jwtTokenService.GenerateToken(user.Id, user.Email, role);
        return Result<AuthResponse>.Success(new AuthResponse(token, user.Email, user.FirstName, user.LastName));
    }
}

