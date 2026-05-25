namespace AHuiHou.Application.DTOs.Responses;

public record PointTransactionResponse(
    Guid Id,
    decimal Amount,
    string TransactionType,
    DateTime CreatedAt
);

