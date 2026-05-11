import { useState, useCallback, type FormEvent } from 'react';

export function useForm<T extends Record<string, unknown>>(
  initialValues: T,
  validate: (values: T) => Partial<Record<keyof T, string>>,
  onSubmit: (values: T) => Promise<void>,
) {
  const [values, setValues] = useState<T>(initialValues);
  const [errors, setErrors] = useState<Partial<Record<keyof T, string>>>({});
  const [touched, setTouched] = useState<Partial<Record<keyof T, boolean>>>({});
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = useCallback(
    (field: keyof T) => (e: { target: { value: unknown } }) => {
      const raw = e.target.value;
      setValues((prev) => ({ ...prev, [field]: raw }));
      setTouched((prev) => ({ ...prev, [field]: true }));
      setErrors((prev) => {
        const updated = { ...prev };
        delete updated[field];
        return updated;
      });
    },
    [],
  );

  const handleBlur = useCallback(
    (field: keyof T) => () => {
      setTouched((prev) => ({ ...prev, [field]: true }));
      const validationErrors = validate(values);
      if (validationErrors[field]) {
        setErrors((prev) => ({ ...prev, [field]: validationErrors[field] }));
      }
    },
    [values, validate],
  );

  const handleSubmit = useCallback(
    async (e: FormEvent) => {
      e.preventDefault();
      const validationErrors = validate(values);
      setErrors(validationErrors);
      setTouched(
        Object.keys(values as object).reduce((acc, k) => ({ ...acc, [k]: true }), {} as Partial<Record<keyof T, boolean>>),
      );

      if (Object.keys(validationErrors).length > 0) return;

      setIsSubmitting(true);
      try {
        await onSubmit(values);
      } finally {
        setIsSubmitting(false);
      }
    },
    [values, validate, onSubmit],
  );

  return { values, errors, touched, handleChange, handleBlur, handleSubmit, isSubmitting };
}
