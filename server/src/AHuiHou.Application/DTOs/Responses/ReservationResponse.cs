namespace AHuiHou.Application.DTOs.Responses;

public record ReservationResponse(
    Guid Id,
    int TableId,
    string TableNumber,
    string AreaName,
    DateTime StartTime,
    DateTime EndTime,
    string Status
);

