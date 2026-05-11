import { useLocation, Link } from 'react-router-dom';
import { formatCurrency } from '@/core/utils/formatCurrency';
import { formatDate } from '@/core/utils/formatDate';
import { Button } from '@/components/ui/Button';
import type { OrderResult } from '@/core/types/models';
import { CheckCircle } from 'lucide-react';
import styles from './OrderConfirmation.module.css';

export default function OrderConfirmation() {
  const location = useLocation();
  const order = location.state as OrderResult | undefined;

  return (
    <div className={`container ${styles.page}`}>
      <div className={styles.card}>
        <CheckCircle size={56} className={styles.icon} />
        <h1>¡Pedido confirmado!</h1>
        <p className={styles.thanks}>Gracias por tu compra. Tu pedido está siendo preparado.</p>

        {order && (
          <div className={styles.details}>
            <div className={styles.detail}>
              <span>Pedido</span>
              <strong>#{order.orderId.slice(0, 8)}</strong>
            </div>
            <div className={styles.detail}>
              <span>Total</span>
              <strong>{formatCurrency(order.totalAmount)}</strong>
            </div>
            <div className={styles.detail}>
              <span>Puntos ganados</span>
              <strong className={styles.points}>+{order.pointsEarned} pts</strong>
            </div>
            <div className={styles.detail}>
              <span>Fecha</span>
              <strong>{formatDate(order.createdAt)}</strong>
            </div>
          </div>
        )}

        <div className={styles.actions}>
          <Link to="/menu">
            <Button variant="primary">Seguir comprando</Button>
          </Link>
          <Link to="/dashboard">
            <Button variant="outline">Ver mi perfil</Button>
          </Link>
        </div>
      </div>
    </div>
  );
}
