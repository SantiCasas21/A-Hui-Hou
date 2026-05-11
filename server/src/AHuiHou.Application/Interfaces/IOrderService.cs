using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IOrderService
{
    Task<Result<OrderResponse>> CreateOrderAsync(Guid userId, CreateOrderRequest request, CancellationToken cancellationToken = default);
}

