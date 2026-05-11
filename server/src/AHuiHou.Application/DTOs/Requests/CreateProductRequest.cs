namespace AHuiHou.Application.DTOs.Requests;

public record CreateProductRequest(
    string Name,
    int CategoryId,
    decimal Price,
    int PointsAwarded
);

