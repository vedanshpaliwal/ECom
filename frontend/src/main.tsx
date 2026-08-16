import React from 'react';
import { createRoot } from 'react-dom/client';
import './styles.css';

function App() {
  return (
    <main className="app-shell">
      <nav className="nav">
        <div className="brand">ECom</div>
        <div className="nav-links"><a href="#shop">Shop</a><a href="#categories">Categories</a><a href="#about">About</a><button>Cart</button></div>
      </nav>
      <section className="hero">
        <div>
          <p className="eyebrow">HANDMADE • THOUGHTFUL • UNIQUE</p>
          <h1>Beautiful things, made with care.</h1>
          <p>Discover woolen flowers, bouquets and resin art designed to make every moment memorable.</p>
          <button className="primary">Explore collection</button>
        </div>
        <div className="hero-art" aria-hidden="true">✿</div>
      </section>
      <section id="categories" className="section"><p className="eyebrow">EXPLORE</p><h2>Shop by category</h2><div className="category-grid"><article>Woolen Flowers</article><article>Flower Bouquets</article><article>Resin Art</article></div></section>
      <section id="shop" className="section"><p className="eyebrow">CURATED FOR YOU</p><h2>Featured products</h2><div className="product-placeholder">Products will appear here once the API is connected.</div></section>
      <footer id="about">© {new Date().getFullYear()} ECom · Handmade with love · India</footer>
    </main>
  );
}

createRoot(document.getElementById('root')!).render(<React.StrictMode><App /></React.StrictMode>);
