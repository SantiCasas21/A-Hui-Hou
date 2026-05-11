import { useNavigate } from 'react-router-dom';
import { motion } from 'framer-motion';
import { Button } from '@/components/ui/Button';
import { Coffee, CalendarDays } from 'lucide-react';
import styles from './HeroSection.module.css';

export function HeroSection() {
  const navigate = useNavigate();

  return (
    <section className={styles.hero}>
      <div className={styles.overlay} />
      <div className={`container ${styles.content}`}>
        <motion.div
          initial={{ opacity: 0, y: 40 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.8, ease: 'easeOut' }}
        >
          <div className={styles.badge}>
            <Coffee size={16} />
            <span>Specialty Coffee desde 2018</span>
          </div>
          <h1 className={styles.title}>
            Donde cada taza<br />cuenta una historia
          </h1>
          <p className={styles.subtitle}>
            Café de especialidad, espacio de coworking y una comunidad que te espera.
            Reserva tu mesa y descubre tu nuevo rincón favorito.
          </p>
          <div className={styles.actions}>
            <Button variant="primary" size="lg" onClick={() => navigate('/menu')}>
              Ver menú
            </Button>
            <Button variant="outline" size="lg" onClick={() => navigate('/reservations')}>
              <CalendarDays size={20} />
              Reservar mesa
            </Button>
          </div>
        </motion.div>
      </div>
    </section>
  );
}
