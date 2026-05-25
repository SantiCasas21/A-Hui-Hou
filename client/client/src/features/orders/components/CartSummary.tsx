import { useCart } from '@/core/hooks/useCart';
import { formatCurrency, formatPoints } from '@/core/utils/formatCurrency';
import styles from './CartSummary.module.css';

export function CartSummary() {
  const { items, subtotal, itemCount } = useCart();

  if (items.length === 0) {
    return (
      <div className={styles.empty}>
        <p>Tu carrito está vacío.</p>
        <p className={styles.hint}>Agrega productos desde el menú.</p>
      </div>
    );
  }

  return (
    <div className={styles.summary}>
      <div className={styles.row}>
        <span>Productos ({itemCount})</span>
        <span>{formatCurrency(subtotal)}</span>
      </div>
      <div className={`${styles.row} ${styles.total}`}>
        <span>Total</span>
        <span>{formatCurrency(subtotal)}</span>
      </div>
      <p className={styles.points}>Ganarás aproximadamente {formatPoints(subtotal * 0.05)} puntos con esta compra.</p>
    </div>
  );
}
