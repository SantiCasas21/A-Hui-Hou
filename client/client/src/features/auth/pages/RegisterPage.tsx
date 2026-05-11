import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '@/core/hooks/useAuth';
import { AuthForm } from '../components/AuthForm';
import type { RegisterRequest } from '@/core/types/requests';
import { ApiClientError } from '@/core/api/client';

export default function RegisterPage() {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (values: RegisterRequest) => {
    setError(null);
    try {
      await register(values);
      navigate('/');
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al registrarse');
    }
  };

  return <AuthForm mode="register" onSubmit={(v) => handleSubmit(v as RegisterRequest)} error={error} />;
}
