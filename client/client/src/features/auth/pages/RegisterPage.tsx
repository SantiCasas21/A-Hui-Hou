import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { useAuth } from '@/core/hooks/useAuth';
import { useForm } from '@/core/hooks/useForm';
import { required, email, minLength } from '@/core/utils/validators';
import { Input } from '@/components/ui/Input';
import type { RegisterRequest } from '@/core/types/requests';
import { ApiClientError } from '@/core/api/client';
import { Coffee } from 'lucide-react';
import { motion } from 'framer-motion';

const REGISTER_IMAGE = 'https://images.unsplash.com/photo-1521017432531-fbd92d768814?w=900&q=80';

export default function RegisterPage() {
  const { register } = useAuth();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const initialValues = { firstName: '', lastName: '', email: '', password: '' };

  const validate = (v: typeof initialValues) => {
    const errs: Record<string, string> = {};
    const r1 = required(v.firstName);
    if (r1) errs.firstName = r1;
    const r2 = required(v.lastName);
    if (r2) errs.lastName = r2;
    const e = email(v.email);
    if (e) errs.email = e;
    const p = minLength(6)(v.password);
    if (p) errs.password = p;
    return errs;
  };

  const { values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting } = useForm(
    initialValues,
    validate,
    async (v) => {
      setError(null);
      try {
        await register(v as RegisterRequest);
        navigate('/');
      } catch (err) {
        setError(err instanceof ApiClientError ? err.message : 'Error al registrarse');
      }
    },
  );

  return (
    <div className="auth-page">
      <div className="auth-image">
        <img src={REGISTER_IMAGE} alt="Espacio de coworking en A Hui Hou" />
        <div className="auth-image-overlay" />
        <div className="auth-image-quote">
          <blockquote>"Tu mesa te está esperando. Café, comunidad y concentración en Chapinero Alto."</blockquote>
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
            <h2>Únete al club</h2>
            <p>Crea tu cuenta y empieza a acumular puntos con cada visita</p>
          </div>

          {error && <div className="auth-server-error">{error}</div>}

          <form onSubmit={handleSubmit} className="auth-form" noValidate>
            <div className="auth-form-row">
              <Input
                label="Nombre"
                value={values.firstName}
                onChange={handleChange('firstName' as never)}
                onBlur={handleBlur('firstName' as never)}
                error={touched.firstName ? errors.firstName : undefined}
                autoComplete="given-name"
              />
              <Input
                label="Apellido"
                value={values.lastName}
                onChange={handleChange('lastName' as never)}
                onBlur={handleBlur('lastName' as never)}
                error={touched.lastName ? errors.lastName : undefined}
                autoComplete="family-name"
              />
            </div>
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
              autoComplete="new-password"
            />
            <button type="submit" className="auth-submit" disabled={isSubmitting}>
              {isSubmitting ? 'Creando cuenta…' : 'Crear cuenta'}
            </button>
          </form>

          <p className="auth-switch">
            ¿Ya tienes cuenta? <Link to="/login">Inicia sesión</Link>
          </p>
        </motion.div>
      </div>
    </div>
  );
}
