import { lazy, Suspense } from 'react'
import { Routes, Route } from 'react-router-dom'
import { PageLayout } from '@/components/layout/PageLayout'
import { ProtectedRoute } from '@/components/ui/ProtectedRoute'
import { Spinner } from '@/components/ui/Spinner'
import styles from '@/components/ui/Spinner.module.css'

const HomePage = lazy(() => import('@/features/home/pages/HomePage'))
const LoginPage = lazy(() => import('@/features/auth/pages/LoginPage'))
const RegisterPage = lazy(() => import('@/features/auth/pages/RegisterPage'))
const MenuPage = lazy(() => import('@/features/menu/pages/MenuPage'))
const ReservationsPage = lazy(() => import('@/features/reservations/pages/ReservationsPage'))
const CheckoutPage = lazy(() => import('@/features/orders/pages/CheckoutPage'))
const OrderConfirmation = lazy(() => import('@/features/orders/pages/OrderConfirmation'))
const DashboardPage = lazy(() => import('@/features/dashboard/pages/DashboardPage'))
const LoyaltyPage = lazy(() => import('@/features/loyalty/pages/LoyaltyPage'))

function LazyFallback() {
  return (
    <div className={styles.spinner} style={{ display: 'flex', justifyContent: 'center', padding: '4rem' }}>
      <Spinner size="lg" />
    </div>
  )
}

export default function App() {
  return (
    <Suspense fallback={<LazyFallback />}>
      <Routes>
        <Route element={<PageLayout />}>
          <Route path="/" element={<HomePage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/menu" element={<MenuPage />} />
          <Route
            path="/reservations"
            element={<ProtectedRoute><ReservationsPage /></ProtectedRoute>}
          />
          <Route
            path="/checkout"
            element={<ProtectedRoute><CheckoutPage /></ProtectedRoute>}
          />
          <Route
            path="/order/:orderId"
            element={<ProtectedRoute><OrderConfirmation /></ProtectedRoute>}
          />
          <Route
            path="/dashboard"
            element={<ProtectedRoute><DashboardPage /></ProtectedRoute>}
          />
          <Route
            path="/loyalty"
            element={<ProtectedRoute><LoyaltyPage /></ProtectedRoute>}
          />
        </Route>
      </Routes>
    </Suspense>
  )
}
