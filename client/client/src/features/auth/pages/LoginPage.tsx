import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '@/core/hooks/useAuth';
import { AuthForm } from '../components/AuthForm';
import type { LoginRequest } from '@/core/types/requests';
import { ApiClientError } from '@/core/api/client';

export default function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (values: LoginRequest) => {
    setError(null);
    try {
      await login(values.email, values.password);
      navigate('/');
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al iniciar sesión');
    }
  };

  return <AuthForm mode="login" onSubmit={(v) => handleSubmit(v as LoginRequest)} error={error} />;
}
