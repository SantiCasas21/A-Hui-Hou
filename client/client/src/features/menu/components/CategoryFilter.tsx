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
        <button
          key={cat}
          className={`${styles.tab} ${activeCategory === cat ? styles.active : ''}`}
          onClick={() => onSelect(cat)}
        >
          {cat}
        </button>
      ))}
    </div>
  );
}
