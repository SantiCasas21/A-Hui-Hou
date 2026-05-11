export interface ApiResponse<T> {
  success: boolean;
  data: T | null;
  error: string | null;
  errorCode: string | null;
}

export interface ErrorResponse {
  statusCode: number;
  error: string;
  errorCode: string;
}
