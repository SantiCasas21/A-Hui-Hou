export function required(value: unknown): string | undefined {
  if (!value || (typeof value === 'string' && !value.trim())) return 'Este campo es obligatorio';
  return undefined;
}

export function email(value: string): string | undefined {
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) return 'Email no válido';
  return undefined;
}

export function minLength(n: number) {
  return (value: string): string | undefined => {
    if (value.length < n) return `Mínimo ${n} caracteres`;
    return undefined;
  };
}

export function isFutureDate(value: string): string | undefined {
  if (new Date(value) <= new Date()) return 'La fecha debe ser futura';
  return undefined;
}
