using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Enums;
using AHuiHou.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AHuiHou.Application.Services;

public class LoyaltyService : ILoyaltyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<LoyaltyService> _logger;

    public LoyaltyService(IUnitOfWork unitOfWork, ILogger<LoyaltyService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<LoyaltyWalletResponse>> GetBalanceAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var wallet = await _unitOfWork.LoyaltyWallets.GetByUserIdAsync(userId, cancellationToken);
        var balance = wallet?.Balance ?? 0;
        var lastUpdate = wallet?.LastUpdate ?? DateTime.UtcNow;

        return Result<LoyaltyWalletResponse>.Success(new LoyaltyWalletResponse(balance, lastUpdate));
    }

    public async Task<Result<LoyaltyWalletResponse>> RedeemPointsAsync(Guid userId, RedeemPointsRequest request, CancellationToken cancellationToken = default)
    {
        var wallet = await _unitOfWork.LoyaltyWallets.GetByUserIdAsync(userId, cancellationToken);
        if (wallet is null || wallet.Balance < request.Points)
            return Result<LoyaltyWalletResponse>.Failure("Saldo de puntos insuficiente", "INSUFFICIENT_POINTS");

        wallet.Balance -= request.Points;
        wallet.LastUpdate = DateTime.UtcNow;
        _unitOfWork.LoyaltyWallets.Update(wallet);

        var transaction = new PointTransaction
        {
            Id = Guid.NewGuid(),
            WalletId = wallet.UserId,
            Amount = -request.Points,
            TransactionType = TransactionType.Redemption.ToString(),
            CreatedAt = DateTime.UtcNow
        };
        await _unitOfWork.PointTransactions.AddAsync(transaction, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("User {UserId} redeemed {Points} points", userId, request.Points);

        return Result<LoyaltyWalletResponse>.Success(new LoyaltyWalletResponse(wallet.Balance, wallet.LastUpdate));
    }

    public async Task<Result<IEnumerable<PointTransactionResponse>>> GetTransactionsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var transactions = await _unitOfWork.PointTransactions.GetByWalletIdAsync(userId, cancellationToken);
        var response = transactions.Select(t => new PointTransactionResponse(t.Id, t.Amount, t.TransactionType, t.CreatedAt));

        return Result<IEnumerable<PointTransactionResponse>>.Success(response);
    }
}

