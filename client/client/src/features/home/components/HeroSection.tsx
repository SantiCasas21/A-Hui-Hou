import { useNavigate, Link } from 'react-router-dom';
import { motion } from 'framer-motion';
import { Coffee, CalendarDays, Sparkles } from 'lucide-react';
import styles from './HeroSection.module.css';

export function HeroSection() {
  const navigate = useNavigate();

  return (
    <section className={styles.hero}>
      <div className={styles.overlay} />
      <div className={styles.bgPattern} />
      <div className={`container ${styles.content}`}>
        <motion.div
          initial={{ opacity: 0, y: 40 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.9, ease: [0.25, 0.46, 0.45, 0.94] }}
        >
          <div className={styles.badge}>
            <Sparkles size={14} />
            <span>2x1 en tu primer café al registrarte</span>
          </div>
          <h1 className={styles.title}>
            Donde cada taza<br />cuenta una historia
          </h1>
          <p className={styles.subtitle}>
            Café de especialidad, espacio de coworking y una comunidad que te espera.
            Descubre tu nuevo rincón favorito en el corazón de la ciudad.
          </p>
          <div className={styles.actions}>
            <button className="btn-cta" onClick={() => navigate('/register')}>
              <Coffee size={20} />
              Quiero mi café gratis
            </button>
            <button className="btn-cta-outline" onClick={() => navigate('/menu')}>
              <CalendarDays size={20} />
              Explorar menú
            </button>
          </div>
          <p className={styles.hint}>
            <Link to="/login">¿Ya tienes cuenta? Inicia sesión</Link>
          </p>
        </motion.div>
      </div>
      <div className={styles.scroll}>
        <span>Desliza</span>
        <motion.div
          animate={{ y: [0, 8, 0] }}
          transition={{ duration: 1.8, repeat: Infinity }}
        >
          &#8595;
        </motion.div>
      </div>
    </section>
  );
}
