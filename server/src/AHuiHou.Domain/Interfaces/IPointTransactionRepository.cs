using AHuiHou.Domain.Entities;

namespace AHuiHou.Domain.Interfaces;

public interface IPointTransactionRepository : IRepository<PointTransaction>
{
    Task<IEnumerable<PointTransaction>> GetByWalletIdAsync(Guid walletId, CancellationToken cancellationToken = default);
}

