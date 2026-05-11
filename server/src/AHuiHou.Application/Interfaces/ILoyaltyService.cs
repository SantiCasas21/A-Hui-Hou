using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;

namespace AHuiHou.Application.Interfaces;

public interface ILoyaltyService
{
    Task<Result<LoyaltyWalletResponse>> GetBalanceAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Result<LoyaltyWalletResponse>> RedeemPointsAsync(Guid userId, RedeemPointsRequest request, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<PointTransactionResponse>>> GetTransactionsAsync(Guid userId, CancellationToken cancellationToken = default);
}

