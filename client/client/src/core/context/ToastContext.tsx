import { createContext, useCallback, useMemo, useState, type ReactNode } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { CheckCircle2, AlertCircle, X } from 'lucide-react';

type ToastType = 'success' | 'error';

interface Toast {
  id: number;
  message: string;
  type: ToastType;
}

interface ToastContextValue {
  showToast: (message: string, type?: ToastType) => void;
}

const ToastContext = createContext<ToastContextValue | null>(null);

let nextId = 0;

function ToastContainer() {
  const [toasts, setToasts] = useState<Toast[]>([]);

  const addToast = useCallback((message: string, type: ToastType) => {
    const id = nextId++;
    setToasts((prev) => [...prev, { id, message, type }]);
    setTimeout(() => {
      setToasts((prev) => prev.filter((t) => t.id !== id));
    }, 3500);
  }, []);

  const removeToast = useCallback((id: number) => {
    setToasts((prev) => prev.filter((t) => t.id !== id));
  }, []);

  const showToast = useCallback(
    (message: string, type: ToastType = 'success') => addToast(message, type),
    [addToast],
  );

  const value = useMemo(() => ({ showToast }), [showToast]);

  return {
    value,
    node: (
      <div className="toast-container">
        <AnimatePresence>
          {toasts.map((t) => (
            <motion.div
              key={t.id}
              className={`toast ${t.type === 'success' ? 'toast-success' : 'toast-error'}`}
              initial={{ opacity: 0, x: 80, scale: 0.95 }}
              animate={{ opacity: 1, x: 0, scale: 1 }}
              exit={{ opacity: 0, x: 40, scale: 0.95 }}
              transition={{ duration: 0.3, ease: [0.25, 0.46, 0.45, 0.94] }}
            >
              <span className="toast-icon">
                {t.type === 'success' ? <CheckCircle2 size={18} /> : <AlertCircle size={18} />}
              </span>
              <span style={{ flex: 1 }}>{t.message}</span>
              <button onClick={() => removeToast(t.id)} style={{ color: 'white', opacity: 0.6, padding: 0 }}>
                <X size={16} />
              </button>
            </motion.div>
          ))}
        </AnimatePresence>
      </div>
    ),
  };
}

function ToastProvider({ children }: { children: ReactNode }) {
  const { value, node } = ToastContainer();
  return (
    <ToastContext.Provider value={value}>
      {children}
      {node}
    </ToastContext.Provider>
  );
}

export { ToastContext, ToastProvider };
