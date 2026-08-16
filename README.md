# ECom

A full-stack handmade products e-commerce application for flowers, bouquets, woolen crafts, resin art, and other handmade products.

## Stack

- Frontend: React + Vite
- Backend: ASP.NET Core Web API
- Database: PostgreSQL
- Authentication: JWT + role-based authorization
- Payments: Paytm
- Deployment target: Vercel + containerized .NET API + PostgreSQL

## Scope

- Public responsive storefront
- Categories, search, product listing and product details
- Product image gallery, zoom and mobile-friendly viewing
- Customer registration/login
- Cart and checkout
- Indian shipping addresses
- 7–15 day delivery estimate for the prototype
- Orders, payments and payment verification
- Email/WhatsApp order notifications
- Admin product/category management
- Admin order management and status updates
- Customer, order and payment audit history

## Repository structure

```text
frontend/   React customer + admin application
backend/    ASP.NET Core API
database/   PostgreSQL migrations and seed data
docs/       Architecture and API documentation
```

## Development

The project is being built incrementally with the database/API foundation first, followed by the storefront, checkout, admin portal and integrations.

**Never commit Paytm credentials, database passwords, JWT secrets, API keys, or other secrets.** Use environment variables for local and deployed environments.
