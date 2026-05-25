import { Coffee, Clock, MapPin, Camera, Users } from 'lucide-react';
import styles from './Footer.module.css';

export function Footer() {
  return (
    <footer className={styles.footer}>
      <div className={`container ${styles.inner}`}>
        <div className={styles.brand}>
          <div className={styles.logo}>
            <Coffee size={24} />
            <span>A Hui Hou</span>
          </div>
          <p className={styles.tagline}>Specialty Coffee & Coworking</p>
        </div>

        <div className={styles.info}>
          <div className={styles.infoItem}>
            <Clock size={18} />
            <div>
              <p className={styles.infoLabel}>Horario</p>
              <p>Lun–Vie: 7:00–21:00</p>
              <p>Sáb–Dom: 8:00–22:00</p>
            </div>
          </div>
          <div className={styles.infoItem}>
            <MapPin size={18} />
            <div>
              <p className={styles.infoLabel}>Ubicación</p>
              <p>Chapinero Alto, Bogotá, Cundinamarca</p>
            </div>
          </div>
        </div>

        <div className={styles.social}>
          <a href="#" aria-label="Instagram" className={styles.socialLink}>
            <Camera size={20} />
          </a>
          <a href="#" aria-label="Facebook" className={styles.socialLink}>
            <Users size={20} />
          </a>
        </div>
      </div>
      <div className={styles.bottom}>
        <p>&copy; {new Date().getFullYear()} A Hui Hou. Todos los derechos reservados.</p>
      </div>
    </footer>
  );
}
