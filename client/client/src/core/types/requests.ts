export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
}

export interface CreateOrderRequest {
  items: OrderItemRequest[];
}

export interface OrderItemRequest {
  productId: number;
  quantity: number;
}

export interface CreateReservationRequest {
  tableId: number;
  startTime: string;
  endTime: string;
}

export interface CreateMembershipRequest {
  membershipTypeId: number;
  startDate: string;
  endDate?: string;
}

export interface RedeemPointsRequest {
  points: number;
}
