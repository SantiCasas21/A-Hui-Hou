using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IProductService
{
    Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);
}

