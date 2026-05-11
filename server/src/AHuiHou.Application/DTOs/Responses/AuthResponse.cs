namespace AHuiHou.Application.DTOs.Responses;

public record AuthResponse(
    string Token,
    string Email,
    string FirstName,
    string LastName
);

