using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class LoyaltyWalletRepository : Repository<LoyaltyWallet>, ILoyaltyWalletRepository
{
    public LoyaltyWalletRepository(AHuiHouDbContext context) : base(context) { }

    public async Task<LoyaltyWallet?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.LoyaltyWallets
            .FirstOrDefaultAsync(w => w.UserId == userId, cancellationToken);
    }

    public async Task AddPointsAsync(Guid userId, int points, CancellationToken cancellationToken = default)
    {
        var wallet = await GetByUserIdAsync(userId, cancellationToken);
        if (wallet is not null)
        {
            wallet.Balance += points;
            wallet.LastUpdate = DateTime.UtcNow;
            _dbSet.Update(wallet);
        }
    }
}

