import { useApi } from '@/core/hooks/useApi';
import { getReservations, cancelReservation } from '../api';
import { ReservationCard } from '../components/ReservationCard';
import { ReservationForm } from '../components/ReservationForm';
import { useState, useCallback } from 'react';
import { ApiClientError } from '@/core/api/client';
import styles from './ReservationsPage.module.css';

export default function ReservationsPage() {
  const { data: reservations, isLoading, refetch } = useApi(getReservations, []);
  const [cancellingId, setCancellingId] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  const handleCancel = useCallback(async (id: string) => {
    setCancellingId(id);
    setError(null);
    try {
      await cancelReservation(id);
      await refetch();
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al cancelar');
    } finally {
      setCancellingId(null);
    }
  }, [refetch]);

  return (
    <div className={`container ${styles.page}`}>
      <h1 className={styles.title}>Reservas</h1>

      <div className={styles.layout}>
        <section className={styles.formSection}>
          <ReservationForm onSuccess={refetch} />
        </section>

        <section className={styles.listSection}>
          <h2 className={styles.subtitle}>Tus reservas</h2>
          {error && <div className={styles.error}>{error}</div>}
          {isLoading ? (
            <p className={styles.msg}>Cargando...</p>
          ) : reservations && reservations.length > 0 ? (
            <div className={styles.list}>
              {reservations.map((r) => (
                <ReservationCard
                  key={r.id}
                  reservation={r}
                  onCancel={handleCancel}
                  isCancelling={cancellingId === r.id}
                />
              ))}
            </div>
          ) : (
            <p className={styles.msg}>No tienes reservas activas.</p>
          )}
        </section>
      </div>
    </div>
  );
}
