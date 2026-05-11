using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Interfaces;

namespace AHuiHou.Application.Services;

public class TableService : ITableService
{
    private readonly IUnitOfWork _unitOfWork;

    public TableService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<TableResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var tables = await _unitOfWork.Tables.GetAllAsync(cancellationToken);
        var response = tables.Select(t => new TableResponse(
            t.Id, t.TableNumber, t.Capacity, t.HasOutlet, t.AreaId, t.Area?.Name ?? ""));

        return Result<IEnumerable<TableResponse>>.Success(response);
    }

    public async Task<Result<IEnumerable<TableResponse>>> GetByAreaAsync(int areaId, CancellationToken cancellationToken = default)
    {
        var tables = await _unitOfWork.Tables.GetByAreaAsync(areaId, cancellationToken);
        var response = tables.Select(t => new TableResponse(
            t.Id, t.TableNumber, t.Capacity, t.HasOutlet, t.AreaId, t.Area?.Name ?? ""));

        return Result<IEnumerable<TableResponse>>.Success(response);
    }
}

