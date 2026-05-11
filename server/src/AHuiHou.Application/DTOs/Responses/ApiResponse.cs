namespace AHuiHou.Application.DTOs.Responses;

public record ApiResponse<T>(
    bool Success,
    T? Data,
    string? Error,
    string? ErrorCode
);

public record ErrorResponse(
    int StatusCode,
    string Error,
    string ErrorCode
);

