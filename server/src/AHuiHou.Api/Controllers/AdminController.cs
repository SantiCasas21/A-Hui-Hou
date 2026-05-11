using AHuiHou.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet("membership-types")]
    public async Task<IActionResult> GetMembershipTypes()
    {
        var types = await _unitOfWork.MembershipTypes.GetAllAsync();
        return Ok(types);
    }

    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _unitOfWork.Products.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("areas")]
    public async Task<IActionResult> GetAreas()
    {
        var tables = await _unitOfWork.Tables.GetAllAsync();
        return Ok(tables);
    }
}

