import { useCallback, useEffect, useRef, useState } from 'react';

export function useApi<T>(
  fetcher: () => Promise<T>,
  deps: unknown[] = [],
) {
  const [data, setData] = useState<T | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const mountedRef = useRef(true);

  const fetch = useCallback(() => {
    setIsLoading(true);
    setError(null);
    fetcher()
      .then((result) => {
        if (mountedRef.current) setData(result);
      })
      .catch((err: Error) => {
        if (mountedRef.current) setError(err.message ?? 'Error desconocido');
      })
      .finally(() => {
        if (mountedRef.current) setIsLoading(false);
      });
  // eslint-disable-next-line react-hooks/exhaustive-deps
  }, deps);

  useEffect(() => {
    mountedRef.current = true;
    fetch();
    return () => { mountedRef.current = false; };
  }, [fetch]);

  return { data, isLoading, error, refetch: fetch };
}
