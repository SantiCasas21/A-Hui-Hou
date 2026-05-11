import type { Product } from '@/core/types/models';
import { formatCurrency } from '@/core/utils/formatCurrency';
import { useCart } from '@/core/hooks/useCart';
import { Button } from '@/components/ui/Button';
import { Coffee, Plus } from 'lucide-react';
import { motion } from 'framer-motion';
import styles from './ProductCard.module.css';

interface ProductCardProps {
  product: Product;
  index: number;
}

export function ProductCard({ product, index }: ProductCardProps) {
  const { addItem } = useCart();

  return (
    <motion.article
      className={styles.card}
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: index * 0.06, duration: 0.4 }}
    >
      <div className={styles.imageWrap}>
        <Coffee size={40} className={styles.placeholder} />
      </div>
      <div className={styles.body}>
        <span className={styles.category}>{product.categoryName}</span>
        <h3 className={styles.name}>{product.name}</h3>
        <div className={styles.footer}>
          <span className={styles.price}>{formatCurrency(product.price)}</span>
          <Button
            variant="primary"
            size="sm"
            onClick={() => addItem(product)}
            aria-label={`Agregar ${product.name} al carrito`}
          >
            <Plus size={16} />
            Agregar
          </Button>
        </div>
      </div>
    </motion.article>
  );
}
