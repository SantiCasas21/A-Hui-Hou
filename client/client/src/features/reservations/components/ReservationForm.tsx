import { useState, useEffect } from 'react';
import { Button } from '@/components/ui/Button';
import { getTables, createReservation } from '../api';
import { ApiClientError } from '@/core/api/client';
import type { TableInfo, Reservation } from '@/core/types/models';
import { toLocalDatetimeISO } from '@/core/utils/formatDate';
import { CalendarDays } from 'lucide-react';
import styles from './ReservationForm.module.css';

interface ReservationFormProps {
  onSuccess: (reservation: Reservation) => void;
}

export function ReservationForm({ onSuccess }: ReservationFormProps) {
  const [tables, setTables] = useState<TableInfo[]>([]);
  const [selectedTable, setSelectedTable] = useState<number | null>(null);
  const [startTime, setStartTime] = useState('');
  const [endTime, setEndTime] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getTables()
      .then(setTables)
      .catch(() => setError('No se pudieron cargar las mesas'));
  }, []);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!selectedTable || !startTime || !endTime) {
      setError('Completa todos los campos');
      return;
    }
    setError(null);
    setIsSubmitting(true);
    try {
      const res = await createReservation({
        tableId: selectedTable,
        startTime: new Date(startTime).toISOString(),
        endTime: new Date(endTime).toISOString(),
      });
      onSuccess(res);
      setSelectedTable(null);
      setStartTime('');
      setEndTime('');
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al crear reserva');
    } finally {
      setIsSubmitting(false);
    }
  };

  const areas = [...new Set(tables.map((t) => t.areaName))];

  return (
    <form onSubmit={handleSubmit} className={styles.form}>
      <h3 className={styles.heading}>
        <CalendarDays size={22} />
        Nueva reserva
      </h3>

      {error && <div className={styles.error}>{error}</div>}

      <div className={styles.fields}>
        {areas.map((area) => (
          <fieldset key={area} className={styles.areaGroup}>
            <legend className={styles.areaName}>{area}</legend>
            <div className={styles.tableGrid}>
              {tables
                .filter((t) => t.areaName === area)
                .map((t) => (
                  <button
                    key={t.id}
                    type="button"
                    className={`${styles.tableBtn} ${selectedTable === t.id ? styles.selected : ''}`}
                    onClick={() => setSelectedTable(t.id)}
                  >
                    <span className={styles.tableNum}>{t.tableNumber}</span>
                    <span className={styles.capacity}>{t.capacity} pers.</span>
                    {t.hasOutlet && <span className={styles.outlet}>⚡</span>}
                  </button>
                ))}
            </div>
          </fieldset>
        ))}

        <div className={styles.datetimeRow}>
          <label className={styles.label}>
            Inicio
            <input
              type="datetime-local"
              className={styles.datetimeInput}
              value={startTime}
              onChange={(e) => setStartTime(e.target.value)}
              min={toLocalDatetimeISO(new Date())}
            />
          </label>
          <label className={styles.label}>
            Fin
            <input
              type="datetime-local"
              className={styles.datetimeInput}
              value={endTime}
              onChange={(e) => setEndTime(e.target.value)}
              min={startTime || toLocalDatetimeISO(new Date())}
            />
          </label>
        </div>
      </div>

      <Button type="submit" variant="primary" isLoading={isSubmitting} className={styles.submitBtn}>
        Reservar mesa
      </Button>
    </form>
  );
}
