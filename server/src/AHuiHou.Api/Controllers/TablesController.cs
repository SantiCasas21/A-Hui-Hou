using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;
    private readonly IUnitOfWork _unitOfWork;

    public TablesController(ITableService tableService, IUnitOfWork unitOfWork)
    {
        _tableService = tableService;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<TableResponse>>>> GetAll()
    {
        var result = await _tableService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<TableResponse>>(true, result.Value, null, null));
    }

    [HttpGet("area/{areaId}")]
    public async Task<ActionResult<ApiResponse<IEnumerable<TableResponse>>>> GetByArea(int areaId)
    {
        var result = await _tableService.GetByAreaAsync(areaId);
        return Ok(new ApiResponse<IEnumerable<TableResponse>>(true, result.Value, null, null));
    }

    [HttpGet("areas")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<IEnumerable<AreaResponse>>>> GetAreas()
    {
        var areas = await _unitOfWork.Areas.GetAllAsync();
        var tables = await _unitOfWork.Tables.GetAllAsync();
        var tablesByArea = tables.GroupBy(t => t.AreaId).ToDictionary(g => g.Key, g => g.ToList());

        var response = areas.Select(a =>
        {
            var areaTables = tablesByArea.GetValueOrDefault(a.Id, []);
            return new AreaResponse(
                a.Id,
                a.Name,
                a.IsQuietZone,
                areaTables.Count,
                true, // WiFi available in all areas
                areaTables.Any(t => t.HasOutlet)
            );
        });

        return Ok(new ApiResponse<IEnumerable<AreaResponse>>(true, response, null, null));
    }
}

