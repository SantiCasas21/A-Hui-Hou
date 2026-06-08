import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@/core/hooks/useAuth';
import { useForm } from '@/core/hooks/useForm';
import { email, minLength } from '@/core/utils/validators';
import { Input } from '@/components/ui/Input';
import { ApiClientError } from '@/core/api/client';
import { Coffee } from 'lucide-react';
import { motion } from 'framer-motion';

const LOGIN_IMAGE = 'https://images.unsplash.com/photo-1501339847302-ac426a4a7cbb?w=900&q=80';

export default function LoginPage() {
  const { login } = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const validate = (v: typeof initialValues) => {
    const errs: Record<string, string> = {};
    const e = email(v.email);
    if (e) errs.email = e;
    const p = minLength(6)(v.password);
    if (p) errs.password = p;
    return errs;
  };

  const initialValues = { email: '', password: '' };

  const { values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting } = useForm(
    initialValues,
    validate,
    async (v) => {
      setError(null);
      try {
        await login(v.email, v.password);
        navigate('/');
      } catch (err) {
        setError(err instanceof ApiClientError ? err.message : 'Error al iniciar sesión');
      }
    },
  );

  return (
    <div className="auth-page">
      <div className="auth-image">
        <img src={LOGIN_IMAGE} alt="Cafetería A Hui Hou en Chapinero Alto" />
        <div className="auth-image-overlay" />
        <div className="auth-image-quote">
          <blockquote>"El aroma del café recién hecho y la calidez de Chapinero Alto te dan la bienvenida."</blockquote>
          <cite>— A Hui Hou, Bogotá</cite>
        </div>
      </div>

      <div className="auth-form-column">
        <motion.div
          className="auth-form-card"
          initial={{ opacity: 0, x: 30 }}
          animate={{ opacity: 1, x: 0 }}
          transition={{ duration: 0.6, ease: [0.25, 0.46, 0.45, 0.94] }}
        >
          <div className="auth-brand">
            <div className="auth-brand-icon">
              <Coffee size={28} />
            </div>
            <h2>Bienvenido de vuelta</h2>
            <p>Inicia sesión y continúa disfrutando de tu club de café</p>
          </div>

          {error && <div className="auth-server-error">{error}</div>}

          <form onSubmit={handleSubmit} className="auth-form" noValidate>
            <Input
              label="Email"
              type="email"
              value={values.email}
              onChange={handleChange('email' as never)}
              onBlur={handleBlur('email' as never)}
              error={touched.email ? errors.email : undefined}
              autoComplete="email"
            />
            <Input
              label="Contraseña"
              type="password"
              value={values.password}
              onChange={handleChange('password' as never)}
              onBlur={handleBlur('password' as never)}
              error={touched.password ? errors.password : undefined}
              autoComplete="current-password"
            />
            <button type="submit" className="auth-submit" disabled={isSubmitting}>
              {isSubmitting ? 'Ingresando…' : 'Iniciar sesión'}
            </button>
          </form>

          <p className="auth-switch">
            ¿No tienes cuenta? <Link to="/register">Regístrate</Link>
          </p>
        </motion.div>
      </div>
    </div>
  );
}
