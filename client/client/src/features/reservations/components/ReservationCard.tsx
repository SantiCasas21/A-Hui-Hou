import type { Reservation } from '@/core/types/models';
import { formatDateTime } from '@/core/utils/formatDate';
import { Button } from '@/components/ui/Button';
import { MapPin, Clock, XCircle } from 'lucide-react';
import styles from './ReservationCard.module.css';

interface ReservationCardProps {
  reservation: Reservation;
  onCancel: (id: string) => void;
  isCancelling: boolean;
}

export function ReservationCard({ reservation, onCancel, isCancelling }: ReservationCardProps) {
  const isActive = reservation.status !== 'Cancelled';

  return (
    <div className={`${styles.card} ${!isActive ? styles.cancelled : ''}`}>
      <div className={styles.header}>
        <span className={styles.tableNum}>Mesa {reservation.tableNumber}</span>
        <span className={`${styles.status} ${isActive ? styles.activeStatus : styles.cancelledStatus}`}>
          {reservation.status}
        </span>
      </div>
      <div className={styles.info}>
        <div className={styles.infoRow}>
          <MapPin size={16} />
          <span>{reservation.areaName}</span>
        </div>
        <div className={styles.infoRow}>
          <Clock size={16} />
          <span>{formatDateTime(reservation.startTime)} — {formatDateTime(reservation.endTime)}</span>
        </div>
      </div>
      {isActive && (
        <Button
          variant="ghost"
          size="sm"
          className={styles.cancelBtn}
          onClick={() => onCancel(reservation.id)}
          isLoading={isCancelling}
        >
          <XCircle size={16} />
          Cancelar
        </Button>
      )}
    </div>
  );
}
