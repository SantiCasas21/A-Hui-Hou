import { HeroSection } from '../components/HeroSection';
import { PromotionsCarousel } from '../components/PromotionsCarousel';
import { CoworkingPricing } from '../components/CoworkingPricing';
import { GallerySection } from '../components/GallerySection';
import { Link } from 'react-router-dom';
import { ArrowRight, Star, Clock, Wifi } from 'lucide-react';
import { motion } from 'framer-motion';
import styles from './HomePage.module.css';

const highlights = [
  { icon: <Star size={24} />, title: 'Café de Especialidad', text: 'Granos seleccionados de origen único, tostados localmente.' },
  { icon: <Clock size={24} />, title: 'Coworking Flexible', text: 'Espacios con WiFi rápido. Paga por hora, medio día o día completo.' },
  { icon: <Wifi size={24} />, title: 'Programa de Lealtad', text: 'Acumula 5% en puntos por cada compra y canjéalos por café gratis.' },
];

export default function HomePage() {
  return (
    <>
      <HeroSection />
      <PromotionsCarousel />

      <section className={styles.highlights}>
        <div className="container">
          <div className="section-header">
            <span className="section-eyebrow">Por qué elegirnos</span>
            <h2>Una experiencia completa</h2>
          </div>

          <div className={styles.grid}>
            {highlights.map((h, i) => (
              <motion.div
                key={h.title}
                className="card-premium"
                style={{ textAlign: 'center' }}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.12 }}
              >
                <div className={styles.icon}>{h.icon}</div>
                <h3>{h.title}</h3>
                <p>{h.text}</p>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      <CoworkingPricing />
      <GallerySection />

      <section className={styles.cta}>
        <div className="container text-center">
          <motion.div
            initial={{ opacity: 0, y: 20 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
          >
            <span className="section-eyebrow">Te esperamos</span>
            <h2 className={styles.ctaTitle}>¿Listo para visitarnos?</h2>
            <p className={styles.ctaText}>Explora nuestro menú, reserva tu mesa favorita o regístrate para tu primer café gratis.</p>
            <div className={styles.ctaActions}>
              <Link to="/menu" className="btn-cta">
                Ver menú completo <ArrowRight size={18} />
              </Link>
              <Link to="/register" className="btn-cta-outline">
                Registrarme gratis
              </Link>
            </div>
          </motion.div>
        </div>
      </section>
    </>
  );
}
