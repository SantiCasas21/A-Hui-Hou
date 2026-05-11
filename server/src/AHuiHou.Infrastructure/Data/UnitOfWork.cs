using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using AHuiHou.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace AHuiHou.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AHuiHouDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AHuiHouDbContext context)
    {
        _context = context;
        Reservations = new ReservationRepository(context);
        Users = new UserRepository(context);
        Products = new ProductRepository(context);
        LoyaltyWallets = new LoyaltyWalletRepository(context);
        PointTransactions = new PointTransactionRepository(context);
        MembershipTypes = new MembershipTypeRepository(context);
        Tables = new TableRepository(context);
    }

    public IReservationRepository Reservations { get; }
    public IUserRepository Users { get; }
    public IProductRepository Products { get; }
    public ILoyaltyWalletRepository LoyaltyWallets { get; }
    public IPointTransactionRepository PointTransactions { get; }
    public IMembershipTypeRepository MembershipTypes { get; }
    public ITableRepository Tables { get; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        => _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
        => await _context.Set<T>().AddAsync(entity, cancellationToken);

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}

