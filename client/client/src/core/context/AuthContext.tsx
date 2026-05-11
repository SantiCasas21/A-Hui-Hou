import { createContext, useCallback, useEffect, useMemo, useState, type ReactNode } from 'react';
import type { User } from '@/core/types/models';
import type { RegisterRequest } from '@/core/types/requests';
import { apiClient } from '@/core/api/client';
import { TOKEN_STORAGE_KEY } from '@/core/api/config';
import type { AuthResult } from '@/core/types/models';

interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
}

interface AuthContextValue extends AuthState {
  login: (email: string, password: string) => Promise<void>;
  register: (data: RegisterRequest) => Promise<void>;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [state, setState] = useState<AuthState>({
    user: null,
    token: localStorage.getItem(TOKEN_STORAGE_KEY),
    isAuthenticated: false,
    isLoading: true,
  });

  const logout = useCallback(() => {
    localStorage.removeItem(TOKEN_STORAGE_KEY);
    setState({ user: null, token: null, isAuthenticated: false, isLoading: false });
  }, []);

  useEffect(() => {
    const storedToken = localStorage.getItem(TOKEN_STORAGE_KEY);
    if (!storedToken) {
      setState((s) => ({ ...s, isLoading: false }));
      return;
    }

    apiClient
      .get<{ id: string; firstName: string; lastName: string; email: string }>('/users/profile')
      .then((res) => {
        const data = res.data as { id: string; firstName: string; lastName: string; email: string };
        setState({
          user: { email: data.email, firstName: data.firstName, lastName: data.lastName },
          token: storedToken,
          isAuthenticated: true,
          isLoading: false,
        });
      })
      .catch(() => {
        localStorage.removeItem(TOKEN_STORAGE_KEY);
        setState({ user: null, token: null, isAuthenticated: false, isLoading: false });
      });
  }, []);

  useEffect(() => {
    const handleSessionExpired = () => logout();
    window.addEventListener('auth:session-expired', handleSessionExpired);
    return () => window.removeEventListener('auth:session-expired', handleSessionExpired);
  }, [logout]);

  const login = useCallback(async (email: string, password: string) => {
    const res = await apiClient.post<AuthResult>('/auth/login', { email, password });
    const data = res.data as AuthResult;
    localStorage.setItem(TOKEN_STORAGE_KEY, data.token);
    setState({
      user: { email: data.email, firstName: data.firstName, lastName: data.lastName },
      token: data.token,
      isAuthenticated: true,
      isLoading: false,
    });
  }, []);

  const register = useCallback(async (req: RegisterRequest) => {
    const res = await apiClient.post<AuthResult>('/auth/register', req);
    const data = res.data as AuthResult;
    localStorage.setItem(TOKEN_STORAGE_KEY, data.token);
    setState({
      user: { email: data.email, firstName: data.firstName, lastName: data.lastName },
      token: data.token,
      isAuthenticated: true,
      isLoading: false,
    });
  }, []);

  const value = useMemo<AuthContextValue>(
    () => ({ ...state, login, register, logout }),
    [state, login, register, logout],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}
