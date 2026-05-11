namespace AHuiHou.Domain.Entities;

public class LoyaltyWallet
{
    public Guid UserId { get; set; }
    public int Balance { get; set; }
    public DateTime LastUpdate { get; set; }

    public User User { get; set; } = null!;
    public ICollection<PointTransaction> PointTransactions { get; set; } = [];
}

