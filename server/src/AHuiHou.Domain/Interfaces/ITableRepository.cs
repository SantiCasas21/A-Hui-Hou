using AHuiHou.Domain.Entities;

namespace AHuiHou.Domain.Interfaces;

public interface ITableRepository : IRepository<Table>
{
    Task<IEnumerable<Table>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default);
}

