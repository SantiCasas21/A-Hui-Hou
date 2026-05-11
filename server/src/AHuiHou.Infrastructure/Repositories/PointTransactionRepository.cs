using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class PointTransactionRepository : Repository<PointTransaction>, IPointTransactionRepository
{
    public PointTransactionRepository(AHuiHouDbContext context) : base(context) { }

    public async Task<IEnumerable<PointTransaction>> GetByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default)
    {
        return await _context.PointTransactions
            .Where(t => t.WalletId == walletId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}

