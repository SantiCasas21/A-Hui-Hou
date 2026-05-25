import { motion } from 'framer-motion';
import styles from './CategoryFilter.module.css';

interface CategoryFilterProps {
  categories: string[];
  activeCategory: string;
  onSelect: (category: string) => void;
}

export function CategoryFilter({ categories, activeCategory, onSelect }: CategoryFilterProps) {
  return (
    <div className={styles.filter}>
      {categories.map((cat) => (
        <motion.button
          key={cat}
          className={`${styles.tab} ${activeCategory === cat ? styles.active : ''}`}
          onClick={() => onSelect(cat)}
          whileTap={{ scale: 0.95 }}
        >
          {cat}
          {activeCategory === cat && (
            <motion.span
              className={styles.indicator}
              layoutId="activeCategory"
              transition={{ type: 'spring', stiffness: 400, damping: 30 }}
            />
          )}
        </motion.button>
      ))}
    </div>
  );
}
