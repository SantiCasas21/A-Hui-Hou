namespace AHuiHou.Application.DTOs.Responses;

public record OrderResponse(
    Guid OrderId,
    decimal TotalAmount,
    int PointsEarned,
    DateTime CreatedAt
);

