namespace AHuiHou.Application.DTOs.Responses;

public record PromotionResponse(
    int Id,
    string Title,
    string Description,
    string? ImageUrl,
    string? DiscountCode,
    DateTime StartDate,
    DateTime EndDate,
    bool IsActive
);
