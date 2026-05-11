using AHuiHou.Domain.Enums;

namespace AHuiHou.Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int TableId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Status { get; set; } = ReservationStatus.Pending.ToString();

    public User User { get; set; } = null!;
    public Table Table { get; set; } = null!;
}

