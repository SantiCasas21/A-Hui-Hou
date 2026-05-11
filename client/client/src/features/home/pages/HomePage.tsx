import { HeroSection } from '../components/HeroSection';
import { Link } from 'react-router-dom';
import { ArrowRight, Star, Clock, Wifi } from 'lucide-react';
import { motion } from 'framer-motion';
import styles from './HomePage.module.css';

const highlights = [
  { icon: <Star size={24} />, title: 'Café de Especialidad', text: 'Granos seleccionados de origen único, tostados localmente.' },
  { icon: <Clock size={24} />, title: 'Coworking Flexible', text: 'Espacios tranquilos con WiFi rápido. Paga por hora o por día.' },
  { icon: <Wifi size={24} />, title: 'Programa de Lealtad', text: 'Acumula puntos con cada compra y canjéalos por beneficios.' },
];

export default function HomePage() {
  return (
    <>
      <HeroSection />

      <section className={styles.highlights}>
        <div className="container">
          <motion.h2
            className={styles.sectionTitle}
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
          >
            Una experiencia completa
          </motion.h2>

          <div className={styles.grid}>
            {highlights.map((h, i) => (
              <motion.div
                key={h.title}
                className={styles.card}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.15 }}
              >
                <div className={styles.icon}>{h.icon}</div>
                <h3>{h.title}</h3>
                <p>{h.text}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      <section className={styles.cta}>
        <div className="container text-center">
          <h2 className={styles.ctaTitle}>¿Listo para visitarnos?</h2>
          <p className={styles.ctaText}>Explora nuestro menú o reserva tu mesa favorita.</p>
          <Link to="/menu" className={styles.ctaLink}>
            Ver menú completo <ArrowRight size={18} />
          </Link>
        </div>
      </section>
    </>
  );
}
