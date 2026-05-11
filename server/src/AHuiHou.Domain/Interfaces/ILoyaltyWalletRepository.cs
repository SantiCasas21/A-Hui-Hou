using AHuiHou.Domain.Entities;

namespace AHuiHou.Domain.Interfaces;

public interface ILoyaltyWalletRepository : IRepository<LoyaltyWallet>
{
    Task<LoyaltyWallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddPointsAsync(Guid userId, int points, CancellationToken cancellationToken = default);
}

