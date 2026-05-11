using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AHuiHou.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService) => _productService = productService;

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProductResponse>>>> GetAll()
    {
        var result = await _productService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<ProductResponse>>(true, result.Value, null, null));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ApiResponse<ProductResponse>>> Create([FromBody] CreateProductRequest request)
    {
        var result = await _productService.CreateAsync(request);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetAll), new ApiResponse<ProductResponse>(true, result.Value, null, null))
            : BadRequest(new ApiResponse<ProductResponse>(false, null, result.Error, result.ErrorCode));
    }
}

