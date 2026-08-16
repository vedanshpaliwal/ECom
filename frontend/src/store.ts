export type CartItem={id?:number;productId:number;name:string;price:number;quantity:number;image?:string};
const KEY='ecom_cart';
export function getCart():CartItem[]{try{return JSON.parse(localStorage.getItem(KEY)||'[]')}catch{return[]}}
export function saveCart(items:CartItem[]){localStorage.setItem(KEY,JSON.stringify(items));window.dispatchEvent(new Event('cart-changed'));}
export function addToCart(item:CartItem){const cart=getCart();const found=cart.find(x=>x.productId===item.productId);if(found)found.quantity+=item.quantity;else cart.push(item);saveCart(cart);}
export function removeFromCart(productId:number){saveCart(getCart().filter(x=>x.productId!==productId));}
export function cartCount(){return getCart().reduce((n,x)=>n+x.quantity,0)}
export function clearCart(){saveCart([])}
