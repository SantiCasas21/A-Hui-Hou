import axios, { AxiosError, type InternalAxiosRequestConfig } from 'axios';
import { API_BASE_URL, TOKEN_STORAGE_KEY } from './config';
import type { ApiResponse, ErrorResponse } from '@/core/types/api';

export const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: { 'Content-Type': 'application/json' },
  timeout: 15000,
});

apiClient.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    const token = localStorage.getItem(TOKEN_STORAGE_KEY);
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error),
);

apiClient.interceptors.response.use(
  (response) => {
    const body = response.data as ApiResponse<unknown>;
    if (!body.success) {
      throw new ApiClientError(
        body.error ?? 'Error desconocido',
        body.errorCode ?? 'UNKNOWN',
        response.status,
      );
    }
    response.data = body.data;
    return response;
  },
  (error: AxiosError<ErrorResponse>) => {
    if (error.response?.status === 401) {
      localStorage.removeItem(TOKEN_STORAGE_KEY);
      window.dispatchEvent(new CustomEvent('auth:session-expired'));
    }
    const serverError = error.response?.data;
    const message = serverError?.error ?? error.message ?? 'Error de red';
    const code = serverError?.errorCode ?? 'NETWORK_ERROR';
    throw new ApiClientError(message, code, error.response?.status ?? 0);
  },
);

export class ApiClientError extends Error {
  constructor(
    message: string,
    public readonly errorCode: string,
    public readonly statusCode: number,
  ) {
    super(message);
    this.name = 'ApiClientError';
  }
}
