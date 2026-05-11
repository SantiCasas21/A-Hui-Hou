namespace AHuiHou.Application.DTOs.Responses;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt,
    int LoyaltyBalance
);

