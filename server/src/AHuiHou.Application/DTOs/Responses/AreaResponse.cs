namespace AHuiHou.Application.DTOs.Responses;

public record AreaResponse(
    int Id,
    string Name,
    bool IsQuietZone,
    int TableCount,
    bool HasWifi,
    bool HasOutlets
);
