using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TablesController : ControllerBase
{
    private readonly ITableService _tableService;

    public TablesController(ITableService tableService) => _tableService = tableService;

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
}

