using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AHuiHouDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AHuiHouDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public virtual async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.ToListAsync(cancellationToken);

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public virtual void Update(T entity)
        => _dbSet.Update(entity);

    public virtual void Delete(T entity)
        => _dbSet.Remove(entity);
}

