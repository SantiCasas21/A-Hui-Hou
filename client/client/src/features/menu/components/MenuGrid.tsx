import type { Product } from '@/core/types/models';
import { ProductCard } from './ProductCard';
import { CategoryFilter } from './CategoryFilter';
import { useMemo, useState } from 'react';
import styles from './MenuGrid.module.css';

interface MenuGridProps {
  products: Product[];
  isLoading: boolean;
}

export function MenuGrid({ products, isLoading }: MenuGridProps) {
  const [activeCategory, setActiveCategory] = useState('Todos');

  const categories = useMemo(() => {
    const cats = ['Todos', ...new Set(products.map((p) => p.categoryName))];
    return cats;
  }, [products]);

  const filtered = useMemo(() => {
    if (activeCategory === 'Todos') return products;
    return products.filter((p) => p.categoryName === activeCategory);
  }, [products, activeCategory]);

  if (isLoading) {
    return (
      <div className={styles.loading}>
        {Array.from({ length: 6 }).map((_, i) => (
          <div key={i} className={styles.skeleton} />
        ))}
      </div>
    );
  }

  return (
    <div>
      <CategoryFilter
        categories={categories}
        activeCategory={activeCategory}
        onSelect={setActiveCategory}
      />
      {filtered.length === 0 ? (
        <p className={styles.empty}>No hay productos en esta categoría.</p>
      ) : (
        <div className={styles.grid}>
          {filtered.map((product, i) => (
            <ProductCard key={product.id} product={product} index={i} />
          ))}
        </div>
      )}
    </div>
  );
}
