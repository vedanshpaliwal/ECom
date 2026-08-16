const API_BASE=import.meta.env.VITE_API_BASE_URL??'http://localhost:5000';
export const apiBase=API_BASE;
async function request(path:string,options:RequestInit={}){const token=localStorage.getItem('ecom_token');const headers=new Headers(options.headers);if(!headers.has('Content-Type')&&options.body)headers.set('Content-Type','application/json');if(token)headers.set('Authorization',`Bearer ${token}`);const r=await fetch(`${API_BASE}${path}`,{...options,headers});let data:any=null;try{data=await r.json()}catch{}if(!r.ok)throw new Error(data?.message??'Request failed');return data;}
export async function getProducts(search?:string){const q=search?`?search=${encodeURIComponent(search)}`:'';return request(`/api/products${q}`)}
export async function getProduct(id:number){return request(`/api/products/${id}`)}
export async function getCategories(){return request('/api/categories')}
export async function login(email:string,password:string){const data=await request('/api/auth/login',{method:'POST',body:JSON.stringify({email,password})});localStorage.setItem('ecom_token',data.token);localStorage.setItem('ecom_user',JSON.stringify(data.user));return data}
export async function register(name:string,email:string,password:string){const data=await request('/api/auth/register',{method:'POST',body:JSON.stringify({name,email,password})});localStorage.setItem('ecom_token',data.token);localStorage.setItem('ecom_user',JSON.stringify(data.user));return data}
export async function getOrders(){return request('/api/orders')}
export async function getOrder(id:number){return request(`/api/orders/${id}`)}
export async function createOrder(address:any){return request('/api/orders',{method:'POST',body:JSON.stringify(address)})}
export async function initiatePayment(orderId:number){return request(`/api/payments/${orderId}/initiate`,{method:'POST'})}
export function logout(){localStorage.removeItem('ecom_token');localStorage.removeItem('ecom_user');window.dispatchEvent(new Event('auth-changed'))}
export function currentUser(){try{return JSON.parse(localStorage.getItem('ecom_user')||'null')}catch{return null}}
