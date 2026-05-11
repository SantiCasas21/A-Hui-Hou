import { Minus, Plus, Trash2 } from 'lucide-react';
import type { CartItem as CartItemType } from '@/core/types/models';
import { formatCurrency } from '@/core/utils/formatCurrency';
import styles from './CartItem.module.css';

interface CartItemProps {
  item: CartItemType;
  onUpdateQuantity: (productId: number, quantity: number) => void;
  onRemove: (productId: number) => void;
}

export function CartItemRow({ item, onUpdateQuantity, onRemove }: CartItemProps) {
  return (
    <div className={styles.row}>
      <div className={styles.info}>
        <h4>{item.name}</h4>
        <span className={styles.unitPrice}>{formatCurrency(item.price)} c/u</span>
      </div>
      <div className={styles.qty}>
        <button
          className={styles.qtyBtn}
          onClick={() => onUpdateQuantity(item.productId, item.quantity - 1)}
          aria-label="Reducir cantidad"
        >
          <Minus size={14} />
        </button>
        <span className={styles.qtyValue}>{item.quantity}</span>
        <button
          className={styles.qtyBtn}
          onClick={() => onUpdateQuantity(item.productId, item.quantity + 1)}
          aria-label="Aumentar cantidad"
        >
          <Plus size={14} />
        </button>
      </div>
      <span className={styles.total}>{formatCurrency(item.price * item.quantity)}</span>
      <button
        className={styles.remove}
        onClick={() => onRemove(item.productId)}
        aria-label="Eliminar"
      >
        <Trash2 size={16} />
      </button>
    </div>
  );
}
