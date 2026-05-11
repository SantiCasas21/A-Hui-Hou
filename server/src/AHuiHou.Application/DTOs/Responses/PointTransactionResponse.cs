namespace AHuiHou.Application.DTOs.Responses;

public record PointTransactionResponse(
    Guid Id,
    int Amount,
    string TransactionType,
    DateTime CreatedAt
);

