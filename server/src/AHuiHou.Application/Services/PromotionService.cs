using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Interfaces;

namespace AHuiHou.Application.Services;

public class PromotionService : IPromotionService
{
    private readonly IUnitOfWork _unitOfWork;

    public PromotionService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<PromotionResponse>>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        var promotions = await _unitOfWork.Promotions.GetAllAsync(cancellationToken);
        var now = DateTime.UtcNow;

        var active = promotions
            .Where(p => p.IsActive && p.StartDate <= now && p.EndDate >= now)
            .Select(p => new PromotionResponse(
                p.Id, p.Title, p.Description, p.ImageUrl, p.DiscountCode,
                p.StartDate, p.EndDate, p.IsActive));

        return Result<IEnumerable<PromotionResponse>>.Success(active);
    }
}
