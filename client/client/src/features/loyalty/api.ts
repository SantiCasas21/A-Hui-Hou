import { apiClient } from '@/core/api/client';
import type { LoyaltyWallet, PointTransaction } from '@/core/types/models';
import type { RedeemPointsRequest } from '@/core/types/requests';

export function getBalance(): Promise<LoyaltyWallet> {
  return apiClient.get<LoyaltyWallet>('/loyalty/balance').then((r) => r.data as LoyaltyWallet);
}

export function redeemPoints(data: RedeemPointsRequest): Promise<LoyaltyWallet> {
  return apiClient.post<LoyaltyWallet>('/loyalty/redeem', data).then((r) => r.data as LoyaltyWallet);
}

export function getTransactions(): Promise<PointTransaction[]> {
  return apiClient.get<PointTransaction[]>('/loyalty/transactions').then((r) => r.data as PointTransaction[]);
}
