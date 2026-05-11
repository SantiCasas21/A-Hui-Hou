import { useApi } from '@/core/hooks/useApi';
import { getProducts } from '../api';
import { MenuGrid } from '../components/MenuGrid';
import styles from './MenuPage.module.css';

export default function MenuPage() {
  const { data: products, isLoading, error } = useApi(getProducts, []);

  return (
    <div className={`container ${styles.page}`}>
      <h1 className={styles.title}>Nuestro Menú</h1>
      <p className={styles.subtitle}>Café de especialidad, comida artesanal y espacios de coworking</p>
      {error && <p className={styles.error}>Error al cargar el menú: {error}</p>}
      <MenuGrid products={products ?? []} isLoading={isLoading} />
    </div>
  );
}
