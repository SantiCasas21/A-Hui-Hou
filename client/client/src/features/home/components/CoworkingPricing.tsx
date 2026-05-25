import { useApi } from '@/core/hooks/useApi';
import { getAreas } from '../api';
import { formatCurrency } from '@/core/utils/formatCurrency';
import { motion } from 'framer-motion';
import { Wifi, Plug, Volume2, VolumeX, Users, Monitor, Clock } from 'lucide-react';
import styles from './CoworkingPricing.module.css';

const coworkingPlans = [
  { label: 'Hora', price: 8000, icon: Clock, popular: false },
  { label: 'Medio día', price: 25000, icon: Monitor, popular: true },
  { label: 'Día completo', price: 45000, icon: Users, popular: false },
];

export function CoworkingPricing() {
  const { data: areas, isLoading } = useApi<import('@/core/types/models').AreaInfo[]>(
    () => getAreas(),
    [],
  );

  return (
    <section className={styles.section}>
      <div className="container">
        <div className="section-header">
          <span className="section-eyebrow">Espacios de Trabajo</span>
          <h2>Coworking</h2>
          <p className="section-subtitle">
            Espacios diseñados para crear, colaborar y concentrarte. Elige el plan y área que mejor se adapte a ti.
          </p>
        </div>

        <div className={styles.plans}>
          {coworkingPlans.map((plan, i) => (
            <motion.div
              key={plan.label}
              className={`${styles.planCard} ${plan.popular ? styles.planPopular : ''}`}
              initial={{ opacity: 0, y: 30 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ delay: i * 0.12 }}
            >
              {plan.popular && <span className={styles.popularBadge}>Más popular</span>}
              <plan.icon size={24} className={styles.planIcon} />
              <span className={styles.planLabel}>{plan.label}</span>
              <span className={styles.planPrice}>{formatCurrency(plan.price)}</span>
            </motion.div>
          ))}
        </div>

        {isLoading ? (
          <p className="state-loading">Cargando espacios...</p>
        ) : (
          <div className={styles.areasGrid}>
            {(areas ?? []).map((area, i) => (
              <motion.div
                key={area.id}
                className="card-premium"
                style={{ padding: 'var(--space-8)' }}
                initial={{ opacity: 0, y: 24 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ delay: i * 0.1 }}
              >
                <h3 className={styles.areaName}>{area.name}</h3>
                <div className={styles.features}>
                  <div className={styles.feature}>
                    <Wifi size={18} />
                    <span>{area.hasWifi ? 'WiFi de alta velocidad' : 'Sin WiFi'}</span>
                  </div>
                  <div className={styles.feature}>
                    <Plug size={18} />
                    <span>{area.hasOutlets ? 'Enchufes individuales' : 'Sin enchufes'}</span>
                  </div>
                  <div className={styles.feature}>
                    {area.isQuietZone ? <VolumeX size={18} /> : <Volume2 size={18} />}
                    <span>{area.isQuietZone ? 'Ambiente silencioso' : 'Nivel de ruido social'}</span>
                  </div>
                  <div className={styles.feature}>
                    <Users size={18} />
                    <span>{area.tableCount} {area.tableCount === 1 ? 'mesa' : 'mesas'} disponibles</span>
                  </div>
                </div>
              </motion.div>
            ))}
          </div>
        )}
      </div>
    </section>
  );
}
