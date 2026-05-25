import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import { AuthProvider } from '@/core/context/AuthContext'
import { CartProvider } from '@/core/context/CartContext'
import { ToastProvider } from '@/core/context/ToastContext'
import App from '@/App'
import '@/styles/global.css'
import '@/styles/utilities.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <ToastProvider>
          <CartProvider>
            <App />
          </CartProvider>
        </ToastProvider>
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>,
)
