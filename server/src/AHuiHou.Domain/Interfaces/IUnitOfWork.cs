using AHuiHou.Domain.Interfaces;

namespace AHuiHou.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IReservationRepository Reservations { get; }
    IUserRepository Users { get; }
    IProductRepository Products { get; }
    ILoyaltyWalletRepository LoyaltyWallets { get; }
    IPointTransactionRepository PointTransactions { get; }
    IMembershipTypeRepository MembershipTypes { get; }
    ITableRepository Tables { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    Task AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
}

