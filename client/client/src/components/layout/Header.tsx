import { Link, useNavigate } from 'react-router-dom';
import { useAuth } from '@/core/hooks/useAuth';
import { useCart } from '@/core/hooks/useCart';
import { Button } from '@/components/ui/Button';
import { ShoppingCart, User as UserIcon, LogOut, Menu, X, Coffee } from 'lucide-react';
import { useState } from 'react';
import styles from './Header.module.css';

export function Header() {
  const { isAuthenticated, user, logout } = useAuth();
  const { itemCount } = useCart();
  const navigate = useNavigate();
  const [menuOpen, setMenuOpen] = useState(false);

  const handleLogout = () => {
    logout();
    navigate('/');
    setMenuOpen(false);
  };

  return (
    <header className={styles.header}>
      <div className={`container ${styles.inner}`}>
        <Link to="/" className={styles.logo}>
          <Coffee size={28} />
          <span>A Hui Hou</span>
        </Link>

        <button
          className={styles.hamburger}
          onClick={() => setMenuOpen(!menuOpen)}
          aria-label="Toggle menu"
        >
          {menuOpen ? <X size={24} /> : <Menu size={24} />}
        </button>

        <nav className={`${styles.nav} ${menuOpen ? styles.navOpen : ''}`}>
          <Link to="/menu" className={styles.navLink} onClick={() => setMenuOpen(false)}>
            Menú
          </Link>
          {isAuthenticated && (
            <Link to="/reservations" className={styles.navLink} onClick={() => setMenuOpen(false)}>
              Reservas
            </Link>
          )}

          <div className={styles.actions}>
            {isAuthenticated ? (
              <>
                <button
                  className={styles.cartBtn}
                  onClick={() => { navigate('/checkout'); setMenuOpen(false); }}
                  aria-label="Carrito"
                >
                  <ShoppingCart size={22} />
                  {itemCount > 0 && <span className={styles.badge}>{itemCount}</span>}
                </button>

                <div className={styles.userMenu}>
                  <Link
                    to="/dashboard"
                    className={styles.userBtn}
                    onClick={() => setMenuOpen(false)}
                    aria-label="Dashboard"
                    title={`${user?.firstName} ${user?.lastName}`}
                  >
                    <UserIcon size={22} />
                    <span className={styles.userName}>{user?.firstName}</span>
                  </Link>
                  <button
                    className={styles.logoutBtn}
                    onClick={handleLogout}
                    aria-label="Cerrar sesión"
                  >
                    <LogOut size={18} />
                  </button>
                </div>
              </>
            ) : (
              <div className={styles.authLinks}>
                <Button variant="ghost" size="sm" onClick={() => { navigate('/login'); setMenuOpen(false); }}>
                  Iniciar sesión
                </Button>
                <Button variant="primary" size="sm" onClick={() => { navigate('/register'); setMenuOpen(false); }}>
                  Registrarse
                </Button>
              </div>
            )}
          </div>
        </nav>
      </div>
    </header>
  );
}
