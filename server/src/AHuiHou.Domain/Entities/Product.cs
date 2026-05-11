namespace AHuiHou.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PointsAwarded { get; set; }

    public Category Category { get; set; } = null!;
}

