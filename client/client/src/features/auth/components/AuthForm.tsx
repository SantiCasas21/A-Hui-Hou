import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { useForm } from '@/core/hooks/useForm';
import { required, email, minLength } from '@/core/utils/validators';
import type { RegisterRequest, LoginRequest } from '@/core/types/requests';
import { Link } from 'react-router-dom';
import { Coffee } from 'lucide-react';
import styles from './AuthForm.module.css';

type Mode = 'login' | 'register';

interface AuthFormProps {
  mode: Mode;
  onSubmit: (values: LoginRequest | RegisterRequest) => Promise<void>;
  error?: string | null;
}

export function AuthForm({ mode, onSubmit, error }: AuthFormProps) {
  const isLogin = mode === 'login';

  const initialValues = isLogin
    ? { email: '', password: '' }
    : { firstName: '', lastName: '', email: '', password: '' };

  const validate = (v: typeof initialValues) => {
    const errs: Record<string, string> = {};
    if (!isLogin) {
      const r = required((v as RegisterRequest).firstName);
      if (r) errs.firstName = r;
      const r2 = required((v as RegisterRequest).lastName);
      if (r2) errs.lastName = r2;
    }
    const e = email(v.email);
    if (e) errs.email = e;
    const p = minLength(6)(v.password);
    if (p) errs.password = p;
    return errs;
  };

  const { values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting } = useForm(
    initialValues,
    validate,
    async (v) => onSubmit(v as LoginRequest | RegisterRequest),
  );

  return (
    <div className={styles.wrapper}>
      <div className={styles.card}>
        <div className={styles.brand}>
          <Coffee size={36} />
          <h2>A Hui Hou</h2>
          <p>{isLogin ? 'Bienvenido de vuelta' : 'Crea tu cuenta'}</p>
        </div>

        {error && <div className={styles.serverError}>{error}</div>}

        <form onSubmit={handleSubmit} className={styles.form} noValidate>
          {!isLogin && (
            <div className={styles.row}>
              <Input
                label="Nombre"
                value={(values as RegisterRequest).firstName ?? ''}
                onChange={handleChange('firstName' as never)}
                onBlur={handleBlur('firstName' as never)}
                error={touched.firstName ? errors.firstName : undefined}
                autoComplete="given-name"
              />
              <Input
                label="Apellido"
                value={(values as RegisterRequest).lastName ?? ''}
                onChange={handleChange('lastName' as never)}
                onBlur={handleBlur('lastName' as never)}
                error={touched.lastName ? errors.lastName : undefined}
                autoComplete="family-name"
              />
            </div>
          )}

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
            autoComplete={isLogin ? 'current-password' : 'new-password'}
          />

          <Button type="submit" variant="primary" size="lg" isLoading={isSubmitting} className={styles.submitBtn}>
            {isLogin ? 'Iniciar sesión' : 'Crear cuenta'}
          </Button>
        </form>

        <p className={styles.switch}>
          {isLogin ? (
            <>¿No tienes cuenta? <Link to="/register">Regístrate</Link></>
          ) : (
            <>¿Ya tienes cuenta? <Link to="/login">Inicia sesión</Link></>
          )}
        </p>
      </div>
    </div>
  );
}
