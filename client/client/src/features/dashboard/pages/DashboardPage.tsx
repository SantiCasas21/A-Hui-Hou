import { useApi } from '@/core/hooks/useApi';
import { apiClient } from '@/core/api/client';
import type { UserProfile } from '@/core/types/models';
import { formatDate } from '@/core/utils/formatDate';
import { Link } from 'react-router-dom';
import { User, Star, Clock, ArrowRight } from 'lucide-react';
import styles from './DashboardPage.module.css';

export default function DashboardPage() {
  const { data: profile, isLoading, error } = useApi<UserProfile>(
    () => apiClient.get<UserProfile>('/users/profile').then((r) => r.data as UserProfile),
    [],
  );

  if (isLoading) {
    return (
      <div className={`container ${styles.page}`}>
        <p className={styles.msg}>Cargando perfil...</p>
      </div>
    );
  }

  if (error || !profile) {
    return (
      <div className={`container ${styles.page}`}>
        <p className={styles.error}>Error al cargar el perfil: {error}</p>
      </div>
    );
  }

  return (
    <div className={`container ${styles.page}`}>
      <h1 className={styles.title}>Mi cuenta</h1>

      <div className={styles.grid}>
        <div className={styles.card}>
          <div className={styles.cardHeader}>
            <User size={20} />
            <h2>Perfil</h2>
          </div>
          <div className={styles.cardBody}>
            <p><strong>{profile.firstName} {profile.lastName}</strong></p>
            <p className={styles.muted}>{profile.email}</p>
            <p className={styles.muted}>Miembro desde {formatDate(profile.createdAt)}</p>
          </div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardHeader}>
            <Star size={20} />
            <h2>Puntos de lealtad</h2>
          </div>
          <div className={styles.cardBody}>
            <p className={styles.points}>{profile.loyaltyBalance} pts</p>
            <Link to="/loyalty" className={styles.cardLink}>
              Ver historial <ArrowRight size={14} />
            </Link>
          </div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardHeader}>
            <Clock size={20} />
            <h2>Acciones rápidas</h2>
          </div>
          <div className={styles.cardBody}>
            <div className={styles.quickActions}>
              <Link to="/reservations" className={styles.cardLink}>Mis reservas <ArrowRight size={14} /></Link>
              <Link to="/menu" className={styles.cardLink}>Hacer un pedido <ArrowRight size={14} /></Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
