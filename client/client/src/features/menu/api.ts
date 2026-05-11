import { apiClient } from '@/core/api/client';
import type { Product } from '@/core/types/models';

export function getProducts(): Promise<Product[]> {
  return apiClient.get<Product[]>('/products').then((r) => r.data as Product[]);
}
