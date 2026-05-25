import { useApi } from '@/core/hooks/useApi';
import { getPromotions } from '../api';
import { motion, AnimatePresence } from 'framer-motion';
import { Tag, Sparkles, ChevronLeft, ChevronRight, Copy, Check } from 'lucide-react';
import { useState, useEffect, useCallback } from 'react';
import { Modal } from '@/components/ui/Modal';
import styles from './PromotionsCarousel.module.css';

export function PromotionsCarousel() {
  const { data: promotions, isLoading } = useApi<import('@/core/types/models').Promotion[]>(
    () => getPromotions(),
    [],
  );

  const [current, setCurrent] = useState(0);
  const [direction, setDirection] = useState(1);
  const [codeModal, setCodeModal] = useState<string | null>(null);
  const [copied, setCopied] = useState(false);
  const items = promotions ?? [];

  const next = useCallback(() => {
    setDirection(1);
    setCurrent((prev) => (prev + 1) % Math.max(items.length, 1));
  }, [items.length]);

  const prev = useCallback(() => {
    setDirection(-1);
    setCurrent((prev) => (prev - 1 + items.length) % Math.max(items.length, 1));
  }, [items.length]);

  useEffect(() => {
    if (items.length <= 1) return;
    const timer = setInterval(next, 6000);
    return () => clearInterval(timer);
  }, [items.length, next]);

  const handleCopy = async (code: string) => {
    await navigator.clipboard.writeText(code);
    setCopied(true);
    setTimeout(() => setCopied(false), 2000);
  };

  if (isLoading) {
    return (
      <section className="section-padding">
        <div className="container">
          <p className="state-loading">Preparando ofertas especiales...</p>
        </div>
      </section>
    );
  }

  if (items.length === 0) return null;

  return (
    <section className={styles.section}>
      <div className="container">
        <div className="section-header">
          <span className="section-eyebrow">Ofertas Exclusivas</span>
          <h2>Promociones</h2>
          <p className="section-subtitle">Beneficios pensados para que disfrutes cada visita al máximo.</p>
        </div>

        <div className={styles.carousel}>
          <AnimatePresence mode="wait" custom={direction}>
            <motion.div
              key={items[current].id}
              className="card-premium overflow-hidden"
              custom={direction}
              initial={{ opacity: 0, x: direction * 80 }}
              animate={{ opacity: 1, x: 0 }}
              exit={{ opacity: 0, x: direction * -40 }}
              transition={{ duration: 0.45, ease: [0.25, 0.46, 0.45, 0.94] }}
              style={{ display: 'flex', padding: 0, flexDirection: 'column' }}
            >
              <div className={styles.imageWrap}>
                <img
                  src={items[current].imageUrl ?? 'https://images.unsplash.com/photo-1501339847302-ac426a4a7cbb?w=800'}
                  alt={items[current].title}
                  className={styles.image}
                />
                <div className={styles.imageOverlay} />
                {items[current].discountCode && (
                  <button
                    className={styles.codeBadge}
                    onClick={() => setCodeModal(items[current].discountCode)}
                  >
                    <Tag size={14} />
                    {items[current].discountCode}
                  </button>
                )}
              </div>
              <div className={styles.info}>
                <div className="flex-between gap-4" style={{ marginBottom: 'var(--space-3)' }}>
                  <h3 className={styles.title}>{items[current].title}</h3>
                  <span className="badge-premium">
                    <Sparkles size={12} />
                    Activo
                  </span>
                </div>
                <p className={styles.desc}>{items[current].description}</p>
              </div>
            </motion.div>
          </AnimatePresence>
        </div>

        {items.length > 1 && (
          <div className={styles.controls}>
            <button className={styles.arrow} onClick={prev} aria-label="Anterior">
              <ChevronLeft size={20} />
            </button>
            <div className={styles.dots}>
              {items.map((_, i) => (
                <button
                  key={i}
                  className={`${styles.dot} ${i === current ? styles.dotActive : ''}`}
                  onClick={() => { setDirection(i > current ? 1 : -1); setCurrent(i); }}
                  aria-label={`Promoción ${i + 1}`}
                />
              ))}
            </div>
            <button className={styles.arrow} onClick={next} aria-label="Siguiente">
              <ChevronRight size={20} />
            </button>
          </div>
        )}

        <div className={styles.counter}>
          {current + 1} / {items.length}
        </div>
      </div>

      <Modal open={codeModal !== null} onClose={() => { setCodeModal(null); setCopied(false); }}>
        <Tag size={36} style={{ color: 'var(--color-primary)', marginBottom: 'var(--space-4)' }} />
        <h3>Código de descuento</h3>
        <p>Usa este código en tu próxima visita:</p>
        <div
          style={{
            background: 'var(--color-primary-light)',
            borderRadius: 'var(--radius-lg)',
            padding: 'var(--space-5) var(--space-6)',
            margin: 'var(--space-4) 0',
          }}
        >
          <span
            style={{
              fontFamily: 'var(--font-heading)',
              fontSize: '1.75rem',
              fontWeight: 700,
              color: 'var(--color-primary)',
              letterSpacing: '2px',
            }}
          >
            {codeModal}
          </span>
        </div>
        <button
          className="btn-cta"
          style={{ width: '100%', justifyContent: 'center' }}
          onClick={() => codeModal && handleCopy(codeModal)}
        >
          {copied ? <Check size={18} /> : <Copy size={18} />}
          {copied ? 'Copiado' : 'Copiar código'}
        </button>
      </Modal>
    </section>
  );
}
