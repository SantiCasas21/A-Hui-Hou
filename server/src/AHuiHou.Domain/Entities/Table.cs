namespace AHuiHou.Domain.Entities;

public class Table
{
    public int Id { get; set; }
    public int AreaId { get; set; }
    public string TableNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public bool HasOutlet { get; set; } = true;

    public Area Area { get; set; } = null!;
    public ICollection<Reservation> Reservations { get; set; } = [];
}

