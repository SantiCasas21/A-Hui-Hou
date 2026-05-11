import { useApi } from '@/core/hooks/useApi';
import { getBalance, getTransactions, redeemPoints } from '../api';
import { useState } from 'react';
import { Button } from '@/components/ui/Button';
import { Input } from '@/components/ui/Input';
import { formatDate } from '@/core/utils/formatDate';
import { ApiClientError } from '@/core/api/client';
import { Star, TrendingUp, TrendingDown, Gift } from 'lucide-react';
import styles from './LoyaltyPage.module.css';

export default function LoyaltyPage() {
  const { data: wallet, refetch: refetchBalance } = useApi(getBalance, []);
  const { data: transactions, refetch: refetchTxs } = useApi(getTransactions, []);
  const [redeemPoints_, setRedeemPoints] = useState('');
  const [isRedeeming, setIsRedeeming] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState<string | null>(null);

  const handleRedeem = async (e: React.FormEvent) => {
    e.preventDefault();
    const pts = parseInt(redeemPoints_, 10);
    if (!pts || pts <= 0) return;
    setError(null);
    setSuccess(null);
    setIsRedeeming(true);
    try {
      await redeemPoints({ points: pts });
      setSuccess(`¡${pts} puntos canjeados!`);
      setRedeemPoints('');
      await refetchBalance();
      await refetchTxs();
    } catch (err) {
      setError(err instanceof ApiClientError ? err.message : 'Error al canjear');
    } finally {
      setIsRedeeming(false);
    }
  };

  return (
    <div className={`container ${styles.page}`}>
      <h1 className={styles.title}>Programa de Lealtad</h1>

      <div className={styles.layout}>
        <div className={styles.main}>
          <div className={styles.balanceCard}>
            <div className={styles.balanceHeader}>
              <Star size={24} />
              <h2>Tu balance</h2>
            </div>
            <p className={styles.balanceAmount}>{wallet?.balance ?? 0} pts</p>
            {wallet && (
              <p className={styles.lastUpdate}>Última actualización: {formatDate(wallet.lastUpdate)}</p>
            )}
          </div>

          <form onSubmit={handleRedeem} className={styles.redeemForm}>
            <h3><Gift size={18} /> Canjear puntos</h3>
            {error && <div className={styles.msgError}>{error}</div>}
            {success && <div className={styles.msgSuccess}>{success}</div>}
            <div className={styles.redeemRow}>
              <Input
                label="Puntos a canjear"
                type="number"
                min="1"
                value={redeemPoints_}
                onChange={(e) => setRedeemPoints(e.target.value)}
              />
              <Button type="submit" variant="secondary" isLoading={isRedeeming}>
                Canjear
              </Button>
            </div>
          </form>

          <div className={styles.transactions}>
            <h3>Historial de transacciones</h3>
            {transactions && transactions.length > 0 ? (
              <div className={styles.txList}>
                {transactions.map((tx) => (
                  <div key={tx.id} className={styles.txRow}>
                    <div className={styles.txIcon}>
                      {tx.amount > 0 ? <TrendingUp size={18} /> : <TrendingDown size={18} />}
                    </div>
                    <div>
                      <p className={styles.txType}>{tx.transactionType}</p>
                      <p className={styles.txDate}>{formatDate(tx.createdAt)}</p>
                    </div>
                    <span className={tx.amount > 0 ? styles.positive : styles.negative}>
                      {tx.amount > 0 ? '+' : ''}{tx.amount} pts
                    </span>
                  </div>
                ))}
              </div>
            ) : (
              <p className={styles.empty}>No hay transacciones aún.</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
