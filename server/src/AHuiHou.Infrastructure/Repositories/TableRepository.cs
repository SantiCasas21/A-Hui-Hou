using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;
using AHuiHou.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AHuiHou.Infrastructure.Repositories;

public class TableRepository : Repository<Table>, ITableRepository
{
    public TableRepository(AHuiHouDbContext context) : base(context) { }

    public async Task<IEnumerable<Table>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default)
    {
        return await _context.Tables
            .Where(t => t.AreaId == areaId)
            .Include(t => t.Area)
            .ToListAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Table>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tables
            .Include(t => t.Area)
            .ToListAsync(cancellationToken);
    }
}

