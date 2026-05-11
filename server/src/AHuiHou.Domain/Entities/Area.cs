namespace AHuiHou.Domain.Entities;

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsQuietZone { get; set; }

    public ICollection<Table> Tables { get; set; } = [];
}

