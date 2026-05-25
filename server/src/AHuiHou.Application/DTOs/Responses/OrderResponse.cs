namespace AHuiHou.Application.DTOs.Responses;

public record OrderResponse(
    Guid OrderId,
    decimal TotalAmount,
    decimal PointsEarned,
    DateTime CreatedAt
);

