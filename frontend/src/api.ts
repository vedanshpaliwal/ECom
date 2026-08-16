const API_BASE = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5000';

export async function getProducts(search?: string) {
  const url = new URL(`${API_BASE}/api/products`);
  if (search) url.searchParams.set('search', search);
  const response = await fetch(url);
  if (!response.ok) throw new Error('Unable to load products');
  return response.json();
}

export async function login(email: string, password: string) {
  const response = await fetch(`${API_BASE}/api/auth/login`, {
    method: 'POST', headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
  });
  const data = await response.json();
  if (!response.ok) throw new Error(data.message ?? 'Login failed');
  localStorage.setItem('ecom_token', data.token);
  localStorage.setItem('ecom_user', JSON.stringify(data.user));
  return data;
}

export async function register(name: string, email: string, password: string) {
  const response = await fetch(`${API_BASE}/api/auth/register`, {
    method: 'POST', headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name, email, password })
  });
  const data = await response.json();
  if (!response.ok) throw new Error(data.message ?? 'Registration failed');
  localStorage.setItem('ecom_token', data.token);
  localStorage.setItem('ecom_user', JSON.stringify(data.user));
  return data;
}
