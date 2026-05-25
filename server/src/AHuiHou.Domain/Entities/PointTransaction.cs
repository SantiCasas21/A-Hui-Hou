using AHuiHou.Domain.Enums;

namespace AHuiHou.Domain.Entities;

public class PointTransaction
{
    public Guid Id { get; set; }
    public Guid WalletId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; } = Enums.TransactionType.Purchase.ToString();
    public DateTime CreatedAt { get; set; }

    public LoyaltyWallet Wallet { get; set; } = null!;
}

