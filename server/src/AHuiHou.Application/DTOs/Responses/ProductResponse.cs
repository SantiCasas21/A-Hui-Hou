namespace AHuiHou.Application.DTOs.Responses;

public record ProductResponse(
    int Id,
    string Name,
    int CategoryId,
    string CategoryName,
    decimal Price,
    int PointsAwarded
);

