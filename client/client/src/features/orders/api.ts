import { apiClient } from '@/core/api/client';
import type { OrderResult } from '@/core/types/models';
import type { CreateOrderRequest } from '@/core/types/requests';

export function createOrder(data: CreateOrderRequest): Promise<OrderResult> {
  return apiClient.post<OrderResult>('/orders', data).then((r) => r.data as OrderResult);
}
