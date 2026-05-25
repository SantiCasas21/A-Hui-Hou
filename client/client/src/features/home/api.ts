import { apiClient } from '@/core/api/client';
import type { Promotion, AreaInfo } from '@/core/types/models';

export function getPromotions(): Promise<Promotion[]> {
  return apiClient.get<Promotion[]>('/promotions').then((r) => r.data as Promotion[]);
}

export function getAreas(): Promise<AreaInfo[]> {
  return apiClient.get<AreaInfo[]>('/tables/areas').then((r) => r.data as AreaInfo[]);
}
