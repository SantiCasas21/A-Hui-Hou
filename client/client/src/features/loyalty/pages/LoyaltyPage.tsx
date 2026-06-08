import { useApi } from '@/core/hooks/useApi';
import { getBalance, getTransactions, redeemPoints } from '../api';
import { useState } from 'react';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { formatDate } from '@/core/utils/formatDate';
import { formatPoints } from '@/core/utils/formatCurrency';
import { ApiClientError } from '@/core/api/client';
import { Star, TrendingUp, TrendingDown, Gift, Sparkles, ArrowRight } from 'lucide-react';
import { motion } from 'framer-motion';
import { Link } from 'react-router-dom';
import styles from './LoyaltyPage.module.css';

export default function LoyaltyPage() {
  const { data: wallet, refetch: refetchBalance } = useApi(getBalance, []);
  const { data: transactions, refetch: refetchTxs } = useApi(getTransactions, []);
  const [redeemPoints_, setRedeemPoints_] = useState('');
  const [isRedeeming, setIsRedeeming] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleRedeem = async (e: React.FormEvent) => {
    e.preventDefault();
    const pts = parseFloat(redeemPoints_);
    if (!pts || pts <= 0) return;
    setError(null);
    setSuccess(null);
    setIsRedeeming(true);
    try {
      await redeemPoints({ points: pts });
      setSuccess(`¡${formatPoints(pts)} puntos canjeados con éxito!`);
      setRedeemPoints_('');
      await refetchBalance();
      await refetchTxs();
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al canjear');
    } finally {
      setIsRedeeming(false);
    }
  };

  const balance = wallet?.balance ?? 0;
  const canRedeemCoffee = balance >= 100;

  return (
    <div className={`container ${styles.page}`}>
      <div className="section-header" style={{ textAlign: 'left', maxWidth: 'none' }}>
        <span className="section-eyebrow">Lealtad</span>
        <h1>Programa de puntos</h1>
        <p className="section-subtitle" style={{ marginTop: 'var(--space-2)' }}>
          Ganas el 0.5% de cada compra en puntos. 100 puntos = 1 café gratis.
        </p>
      </div>

      <div className={styles.layout}>
        <div className={styles.main}>
          <motion.div
            className={styles.balanceCard}
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
          >
            <div className="flex-between gap-4" style={{ marginBottom: 'var(--space-4)' }}>
              <div className="inline-flex gap-2">
                <Star size={22} style={{ color: 'var(--color-gold)' }} />
                <h2 style={{ color: 'white', fontSize: '1.125rem' }}>Tu balance</h2>
              </div>
              {canRedeemCoffee && (
                <span style={{
                  background: 'rgba(255,255,255,0.2)', color: 'white',
                  padding: '2px 12px', borderRadius: 'var(--radius-full)',
                  fontSize: '0.75rem', fontWeight: 600,
                }}>
                  <Sparkles size={12} style={{ marginRight: 4 }} />
                  Canjeable
                </span>
              )}
            </div>
            <p className={styles.balanceAmount}>{formatPoints(balance)} pts</p>
            {wallet && (
              <p style={{ fontSize: '0.8125rem', opacity: 0.75, color: 'white' }}>
                Actualizado: {formatDate(wallet.lastUpdate)}
              </p>
            )}
            <div className={styles.progressWrap}>
              <div className={styles.progressBar}>
                <div className={styles.progressFill} style={{ width: `${Math.min((balance / 100) * 100, 100)}%` }} />
              </div>
              <p style={{ fontSize: '0.8125rem', opacity: 0.8, color: 'white', marginTop: 'var(--space-2)' }}>
                {canRedeemCoffee
                  ? '¡Ya puedes canjear tu café gratis!'
                  : `Te faltan ${formatPoints(100 - balance)} pts para tu café gratis`}
              </p>
            </div>
          </motion.div>

          <motion.form
            onSubmit={handleRedeem}
            className="card-premium"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.1 }}
          >
            <div className="inline-flex gap-2" style={{ marginBottom: 'var(--space-4)' }}>
              <Gift size={18} style={{ color: 'var(--color-primary)' }} />
              <h3>Canjear puntos</h3>
            </div>
            {error && <div className={styles.msgError}>{error}</div>}
            {success && <div className={styles.msgSuccess}>{success}</div>}
            <div className={styles.redeemRow}>
              <Input
                label="Puntos a canjear"
                type="number"
                min="0.1"
                step="0.1"
                value={redeemPoints_}
                onChange={(e) => setRedeemPoints_(e.target.value)}
              />
              <Button type="submit" variant="primary" isLoading={isRedeeming}>
                Canjear
              </Button>
            </div>
            <p style={{ fontSize: '0.8125rem', color: 'var(--color-text-muted)', marginTop: 'var(--space-3)' }}>
              Cada 100 puntos = 1 café gratis
            </p>
          </motion.form>

          <motion.div
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2 }}
          >
            <h3 style={{ marginBottom: 'var(--space-5)' }}>Historial de transacciones</h3>
            {transactions && transactions.length > 0 ? (
              <div className={styles.txList}>
                {transactions.map((tx) => (
                  <div key={tx.id} className={styles.txRow}>
                    <div className={styles.txIcon}>
                      {tx.amount > 0 ? <TrendingUp size={18} /> : <TrendingDown size={18} />}
                    </div>
                    <div>
                      <p className={styles.txType}>{tx.transactionType === 'Purchase' ? 'Compra' : 'Canje'}</p>
                      <p className={styles.txDate}>{formatDate(tx.createdAt)}</p>
                    </div>
                    <span className={tx.amount > 0 ? styles.positive : styles.negative}>
                      {tx.amount > 0 ? '+' : ''}{formatPoints(tx.amount)} pts
                    </span>
                  </div>
                ))}
              </div>
            ) : (
              <div className={styles.emptyState}>
                <p style={{ color: 'var(--color-text-muted)', marginBottom: 'var(--space-4)' }}>
                  Aún no tienes transacciones.
                </p>
                <Link to="/menu" className="btn-cta">
                  Ir al menú <ArrowRight size={16} />
                </Link>
              </div>
            )}
          </motion.div>
        </div>
      </div>
    </div>
  );
}
