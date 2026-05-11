import { apiClient } from '@/core/api/client';
import type { AuthResult } from '@/core/types/models';
import type { LoginRequest, RegisterRequest } from '@/core/types/requests';

export function loginUser(data: LoginRequest): Promise<AuthResult> {
  return apiClient.post<AuthResult>('/auth/login', data).then((r) => r.data as AuthResult);
}

export function registerUser(data: RegisterRequest): Promise<AuthResult> {
  return apiClient.post<AuthResult>('/auth/register', data).then((r) => r.data as AuthResult);
}
