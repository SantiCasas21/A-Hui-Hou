using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface IPromotionService
{
    Task<Result<IEnumerable<PromotionResponse>>> GetActiveAsync(CancellationToken cancellationToken = default);
}
