import { apiClient } from '@/core/api/client';
import type { Reservation, TableInfo } from '@/core/types/models';
import type { CreateReservationRequest } from '@/core/types/requests';

export function getReservations(): Promise<Reservation[]> {
  return apiClient.get<Reservation[]>('/reservations').then((r) => r.data as Reservation[]);
}

export function createReservation(data: CreateReservationRequest): Promise<Reservation> {
  return apiClient.post<Reservation>('/reservations', data).then((r) => r.data as Reservation);
}

export function cancelReservation(id: string): Promise<null> {
  return apiClient.put<null>(`/reservations/${id}/cancel`).then((r) => r.data as null);
}

export function getTables(): Promise<TableInfo[]> {
  return apiClient.get<TableInfo[]>('/tables').then((r) => r.data as TableInfo[]);
}
