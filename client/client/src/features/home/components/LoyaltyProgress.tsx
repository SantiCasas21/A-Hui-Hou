import { Link } from 'react-router-dom';
import { Gift, ArrowRight, Coffee, Sparkles } from 'lucide-react';
import { motion } from 'framer-motion';
import { formatPoints } from '@/core/utils/formatCurrency';

const POINTS_FOR_FREE_COFFEE = 100;

interface LoyaltyProgressProps {
  balance: number;
}

export function LoyaltyProgress({ balance }: LoyaltyProgressProps) {
  const progress = Math.min((balance / POINTS_FOR_FREE_COFFEE) * 100, 100);
  const missing = Math.max(POINTS_FOR_FREE_COFFEE - balance, 0);
  const canRedeem = balance >= POINTS_FOR_FREE_COFFEE;

  return (
    <motion.div
      className="card-premium"
      style={{ border: canRedeem ? '2px solid var(--color-primary)' : undefined }}
      initial={{ opacity: 0, y: 16 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ delay: 0.2 }}
    >
      <div className="flex-between gap-4" style={{ marginBottom: 'var(--space-5)' }}>
        <div className="inline-flex gap-2">
          <Gift size={20} style={{ color: 'var(--color-primary)' }} />
          <h2 style={{ fontSize: '1.125rem' }}>Tu progreso</h2>
        </div>
        {canRedeem && (
          <span className="badge-premium">
            <Sparkles size={12} />
            Canjeable
          </span>
        )}
      </div>

      <div>
        <p style={{ marginBottom: 'var(--space-1)' }}>
          <span style={{ fontSize: '2.5rem', fontWeight: 700, color: 'var(--color-primary)', fontFamily: 'var(--font-heading)' }}>
            {formatPoints(balance)}
          </span>
          <span style={{ color: 'var(--color-text-muted)', marginLeft: 'var(--space-2)' }}>puntos</span>
        </p>

        <div style={{
          height: '12px',
          background: 'var(--color-primary-light)',
          borderRadius: 'var(--radius-full)',
          overflow: 'hidden',
          marginBottom: 'var(--space-3)',
        }}>
          <motion.div
            style={{
              height: '100%',
              background: progress >= 100
                ? 'linear-gradient(90deg, var(--color-primary), var(--color-secondary))'
                : 'var(--color-primary)',
              borderRadius: 'var(--radius-full)',
            }}
            initial={{ width: 0 }}
            animate={{ width: `${progress}%` }}
            transition={{ duration: 1.2, ease: [0.25, 0.46, 0.45, 0.94] }}
          />
        </div>

        <div className="flex-between gap-4">
          <p style={{ color: 'var(--color-text-muted)', fontSize: '0.9375rem' }}>
            {canRedeem
              ? '¡Ya puedes canjear tu café gratis!'
              : `Te faltan ${formatPoints(missing)} puntos para tu próximo café gratis`}
          </p>
          <span style={{ fontSize: '0.8125rem', fontWeight: 600, color: 'var(--color-primary)' }}>
            {Math.round(progress)}%
          </span>
        </div>
      </div>

      {canRedeem && (
        <Link
          to="/loyalty"
          className="btn-cta"
          style={{ width: '100%', justifyContent: 'center', marginTop: 'var(--space-5)' }}
        >
          <Coffee size={18} />
          Canjear puntos ahora
          <ArrowRight size={16} />
        </Link>
      )}

      {!canRedeem && (
        <Link
          to="/menu"
          style={{
            display: 'inline-flex', alignItems: 'center', gap: 'var(--space-2)',
            marginTop: 'var(--space-4)', fontWeight: 600, fontSize: '0.9375rem',
            color: 'var(--color-accent)',
          }}
        >
          Ganar más puntos <ArrowRight size={14} />
        </Link>
      )}
    </motion.div>
  );
}
