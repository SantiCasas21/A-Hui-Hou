namespace AHuiHou.Application.DTOs.Responses;

public record LoyaltyWalletResponse(
    decimal Balance,
    DateTime LastUpdate
);

