namespace AHuiHou.Application.DTOs.Requests;

public record CreateReservationRequest(
    int TableId,
    DateTime StartTime,
    DateTime EndTime
);

