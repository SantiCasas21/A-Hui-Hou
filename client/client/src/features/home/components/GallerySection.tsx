import { motion } from 'framer-motion';
import styles from './GallerySection.module.css';

const photos = [
  { src: 'https://images.unsplash.com/photo-1501339847302-ac426a4a7cbb?w=800', alt: 'Interior de la cafetería con mesas de madera' },
  { src: 'https://images.unsplash.com/photo-1554118811-1e0d58224f24?w=800', alt: 'Barista preparando café de especialidad' },
  { src: 'https://images.unsplash.com/photo-1445116572660-236099ec97a0?w=800', alt: 'Espacio de coworking con luz natural' },
  { src: 'https://images.unsplash.com/photo-1509042239860-f550ce710b93?w=800', alt: 'Arte latte sobre mesa rústica' },
  { src: 'https://images.unsplash.com/photo-1517701550927-30cf4ba1dba5?w=800', alt: 'Taza de cappuccino con canela' },
  { src: 'https://images.unsplash.com/photo-1414235077428-338989a2e8c0?w=800', alt: 'Ambiente acogedor con iluminación cálida' },
];

export function GallerySection() {
  return (
    <section className={styles.section}>
      <div className="container">
        <div className="section-header">
          <span className="section-eyebrow">Galería</span>
          <h2>Nuestro espacio</h2>
          <p className="section-subtitle">
            Un rincón cuidado al detalle donde el café, el diseño y la comunidad se encuentran.
          </p>
        </div>

        <div className={styles.gallery}>
          {photos.map((photo, i) => (
            <motion.div
              key={photo.src}
              className={styles.item}
              initial={{ opacity: 0, scale: 0.94 }}
              whileInView={{ opacity: 1, scale: 1 }}
              viewport={{ once: true, margin: '-80px' }}
              transition={{ delay: i * 0.08, duration: 0.5, ease: [0.25, 0.46, 0.45, 0.94] }}
            >
              <img src={photo.src} alt={photo.alt} className={styles.image} loading="lazy" />
              <div className={styles.caption}>{photo.alt}</div>
            </motion.div>
          ))}
        </div>
      </div>
    </section>
  );
}
