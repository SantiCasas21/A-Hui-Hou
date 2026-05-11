namespace AHuiHou.Application.DTOs.Responses;

public record TableResponse(
    int Id,
    string TableNumber,
    int Capacity,
    bool HasOutlet,
    int AreaId,
    string AreaName
);

