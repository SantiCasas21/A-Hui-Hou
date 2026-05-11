using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface ITableService
{
    Task<Result<IEnumerable<TableResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<TableResponse>>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default);
}

