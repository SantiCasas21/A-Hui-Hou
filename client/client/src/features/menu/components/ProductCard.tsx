import { useState } from 'react';
import type { Product } from '@/core/types/models';
import { formatCurrency } from '@/core/utils/formatCurrency';
import { useCart } from '@/core/hooks/useCart';
import { useAuth } from '@/core/hooks/useAuth';
import { useToast } from '@/core/hooks/useToast';
import { Modal } from '@/components/ui/Modal';
import { Button } from '@/components/ui/Button';
import { Coffee, Plus, Star, LogIn, UserPlus } from 'lucide-react';
import { Link } from 'react-router-dom';
import { motion } from 'framer-motion';
import styles from './ProductCard.module.css';

interface ProductCardProps {
  product: Product;
  index: number;
}

export function ProductCard({ product, index }: ProductCardProps) {
  const { addItem } = useCart();
  const { isAuthenticated } = useAuth();
  const { showToast } = useToast();
  const [showAuthModal, setShowAuthModal] = useState(false);

  const handleAdd = () => {
    if (!isAuthenticated) {
      setShowAuthModal(true);
      return;
    }
    addItem(product);
    showToast(`${product.name} agregado al carrito`);
  };

  return (
    <>
      <motion.article
        className={styles.card}
        initial={{ opacity: 0, y: 24 }}
        animate={{ opacity: 1, y: 0 }}
        transition={{ delay: index * 0.05, duration: 0.45, ease: [0.25, 0.46, 0.45, 0.94] }}
      >
        <div className={styles.imageWrap}>
          <Coffee size={36} className={styles.placeholder} />
          {product.pointsAwarded > 0 && (
            <span className={styles.pointsBadge}>
              <Star size={11} />+{product.pointsAwarded} pts
            </span>
          )}
        </div>
        <div className={styles.body}>
          <span className={styles.category}>{product.categoryName}</span>
          <h3 className={styles.name}>{product.name}</h3>
          <div className={styles.footer}>
            <span className={styles.price}>{formatCurrency(product.price)}</span>
            <Button
              variant="primary"
              size="sm"
              onClick={handleAdd}
              aria-label={`Agregar ${product.name} al carrito`}
            >
              <Plus size={16} />
              Agregar
            </Button>
          </div>
        </div>
      </motion.article>

      <Modal open={showAuthModal} onClose={() => setShowAuthModal(false)}>
        <Coffee size={40} style={{ color: 'var(--color-primary)', marginBottom: 'var(--space-4)' }} />
        <h3>¿Te gusta lo que ves?</h3>
        <p>
          Regístrate para acumular puntos y hacer tu pedido.
          Cada compra te da el 0.5% en puntos de lealtad.
        </p>
        <div style={{ display: 'flex', flexDirection: 'column', gap: 'var(--space-3)' }}>
          <Link
            to="/login"
            className="btn-cta"
            style={{ justifyContent: 'center' }}
            onClick={() => setShowAuthModal(false)}
          >
            <LogIn size={18} />
            Iniciar sesión
          </Link>
          <Link
            to="/register"
            className="btn-cta-outline"
            style={{ justifyContent: 'center' }}
            onClick={() => setShowAuthModal(false)}
          >
            <UserPlus size={18} />
            Crear cuenta
          </Link>
        </div>
      </Modal>
    </>
  );
}
