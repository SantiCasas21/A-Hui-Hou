using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Enums;
using AHuiHou.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AHuiHou.Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<OrderResponse>> CreateOrderAsync(Guid userId, CreateOrderRequest request, CancellationToken cancellationToken = default)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            decimal totalAmount = 0;

            foreach (var item in request.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId, cancellationToken);
                if (product is null)
                    return Result<OrderResponse>.Failure($"Product with ID {item.ProductId} not found", "PRODUCT_NOT_FOUND");

                totalAmount += product.Price * item.Quantity;
            }

            // Points = 5% of total purchase (with decimals)
            decimal pointsEarned = totalAmount * 0.05m;

            var wallet = await _unitOfWork.LoyaltyWallets.GetByUserIdAsync(userId, cancellationToken);
            if (wallet is null)
            {
                wallet = new LoyaltyWallet
                {
                    UserId = userId,
                    Balance = 0,
                    LastUpdate = DateTime.UtcNow
                };
                await _unitOfWork.LoyaltyWallets.AddAsync(wallet, cancellationToken);
            }

            wallet.Balance += pointsEarned;
            wallet.LastUpdate = DateTime.UtcNow;
            _unitOfWork.LoyaltyWallets.Update(wallet);

            var transaction = new PointTransaction
            {
                Id = Guid.NewGuid(),
                WalletId = wallet.UserId,
                Amount = pointsEarned,
                TransactionType = TransactionType.Purchase.ToString(),
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.PointTransactions.AddAsync(transaction, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Order completed for user {UserId}: Total ${Total}, Points earned: {Points}",
                userId, totalAmount, pointsEarned);

            return Result<OrderResponse>.Success(new OrderResponse(
                Guid.NewGuid(),
                totalAmount,
                pointsEarned,
                DateTime.UtcNow));
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}

