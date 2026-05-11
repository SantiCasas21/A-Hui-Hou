namespace AHuiHou.Application.DTOs.Requests;

public record RegisterUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

