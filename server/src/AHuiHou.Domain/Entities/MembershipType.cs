namespace AHuiHou.Domain.Entities;

public class MembershipType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal DiscountRate { get; set; }
    public decimal MonthlyFee { get; set; }

    public ICollection<UserMembership> UserMemberships { get; set; } = [];
}

