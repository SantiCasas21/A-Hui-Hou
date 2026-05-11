namespace AHuiHou.Application.DTOs.Requests;

public record OrderItemRequest(
    int ProductId,
    int Quantity
);

public record CreateOrderRequest(
    List<OrderItemRequest> Items
);

