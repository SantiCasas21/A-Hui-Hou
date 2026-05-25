import { useCart } from '@/core/hooks/useCart';
import { useAuth } from '@/core/hooks/useAuth';
import { Button } from '@/components/ui/Button';
import { CartItemRow } from '../components/CartItem';
import { CartSummary } from '../components/CartSummary';
import { createOrder } from '../api';
import { formatCurrency } from '@/core/utils/formatCurrency';
import { ApiClientError } from '@/core/api/client';
import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ShoppingBag } from 'lucide-react';
import styles from './CheckoutPage.module.css';

export default function CheckoutPage() {
  const { items, updateQuantity, removeItem, clearCart, subtotal } = useCart();
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async () => {
    if (items.length === 0) return;
    setError(null);
    setIsSubmitting(true);
    try {
      const result = await createOrder({
        items: items.map((i) => ({ productId: i.productId, quantity: i.quantity })),
      });
      clearCart();
      navigate(`/order/${result.orderId}`, { state: result });
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al crear el pedido');
    } finally {
      setIsSubmitting(false);
    }
  };

  if (!isAuthenticated) {
    return (
      <div className={`container ${styles.page}`}>
        <div className={styles.authRequired}>
          <h2>Inicia sesión para continuar</h2>
          <p>Debes iniciar sesión para realizar un pedido.</p>
          <Link to="/login?redirect=/checkout">
            <Button variant="primary">Iniciar sesión</Button>
          </Link>
        </div>
      </div>
    );
  }

  return (
    <div className={`container ${styles.page}`}>
      <h1 className={styles.title}>
        <ShoppingBag size={28} />
        Tu pedido
      </h1>

      {error && <div className={styles.error}>{error}</div>}

      {items.length === 0 ? (
        <div className={styles.empty}>
          <p>No tienes productos en tu carrito.</p>
          <Link to="/menu">
            <Button variant="primary">Ir al menú</Button>
          </Link>
        </div>
      ) : (
        <div className={styles.layout}>
          <div className={styles.items}>
            {items.map((item) => (
              <CartItemRow
                key={item.productId}
                item={item}
                onUpdateQuantity={updateQuantity}
                onRemove={removeItem}
              />
            ))}
          </div>
          <div className={styles.sidebar}>
            <CartSummary />
            <Button
              variant="primary"
              size="lg"
              className={styles.submitBtn}
              isLoading={isSubmitting}
              onClick={handleSubmit}
            >
              Confirmar pedido — {formatCurrency(subtotal)}
            </Button>
          </div>
        </div>
      )}
    </div>
  );
}
