import { useApi } from '@/core/hooks/useApi';
import { apiClient } from '@/core/api/client';
import type { UserProfile } from '@/core/types/models';
import { formatDate } from '@/core/utils/formatDate';
import { formatPoints } from '@/core/utils/formatCurrency';
import { LoyaltyProgress } from '@/features/home/components/LoyaltyProgress';
import { Link } from 'react-router-dom';
import { motion } from 'framer-motion';
import { User, Star, Clock, ArrowRight, CalendarDays, ShoppingBag } from 'lucide-react';
import styles from './DashboardPage.module.css';

export default function DashboardPage() {
  const { data: profile, isLoading, error } = useApi<UserProfile>(
    () => apiClient.get<UserProfile>('/users/profile').then((r) => r.data as UserProfile),
    [],
  );

  if (isLoading) {
    return (
      <div className={`container ${styles.page}`}>
        <p className="state-loading">Cargando perfil...</p>
      </div>
    );
  }

  if (error || !profile) {
    return (
      <div className={`container ${styles.page}`}>
        <p className="state-error">Error al cargar el perfil: {error}</p>
      </div>
    );
  }

  return (
    <div className={`container ${styles.page}`}>
      <div className="section-header" style={{ textAlign: 'left', maxWidth: 'none', marginBottom: 'var(--space-10)' }}>
        <span className="section-eyebrow">Mi cuenta</span>
        <h1>¡Hola, {profile.firstName}!</h1>
      </div>

      <div className={styles.grid}>
        <motion.div
          className="card-premium"
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.05 }}
        >
          <div className="inline-flex gap-3" style={{ marginBottom: 'var(--space-5)', color: 'var(--color-primary)' }}>
            <User size={20} />
            <h2 style={{ fontSize: '1.125rem' }}>Perfil</h2>
          </div>
          <p><strong>{profile.firstName} {profile.lastName}</strong></p>
          <p className="text-muted">{profile.email}</p>
          <p className="text-muted" style={{ fontSize: '0.875rem' }}>Miembro desde {formatDate(profile.createdAt)}</p>
        </motion.div>

        <motion.div
          className="card-premium"
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.1 }}
        >
          <div className="inline-flex gap-3" style={{ marginBottom: 'var(--space-5)', color: 'var(--color-primary)' }}>
            <Star size={20} />
            <h2 style={{ fontSize: '1.125rem' }}>Puntos de lealtad</h2>
          </div>
          <p className={styles.pointsValue}>{formatPoints(profile.loyaltyBalance)} pts</p>
          <Link to="/loyalty" className="inline-flex gap-1" style={{ fontWeight: 600, fontSize: '0.9375rem' }}>
            Ver historial <ArrowRight size={14} />
          </Link>
        </motion.div>

        <motion.div
          className="card-premium"
          initial={{ opacity: 0, y: 20 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ delay: 0.15 }}
        >
          <div className="inline-flex gap-3" style={{ marginBottom: 'var(--space-5)', color: 'var(--color-primary)' }}>
            <Clock size={20} />
            <h2 style={{ fontSize: '1.125rem' }}>Acciones rápidas</h2>
          </div>
          <div className={styles.quickActions}>
            <Link to="/reservations" className={styles.quickLink}>
              <CalendarDays size={16} /> Mis reservas <ArrowRight size={14} />
            </Link>
            <Link to="/menu" className={styles.quickLink}>
              <ShoppingBag size={16} /> Hacer un pedido <ArrowRight size={14} />
            </Link>
          </div>
        </motion.div>
      </div>

      <div className={styles.loyaltySection}>
        <LoyaltyProgress balance={profile.loyaltyBalance} />
      </div>
    </div>
  );
}
