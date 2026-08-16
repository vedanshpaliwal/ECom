# ECom

Full-stack prototype e-commerce application for handmade woolen flowers, bouquets, resin art and similar products.

## Stack

- React + Vite
- ASP.NET Core 8 Web API
- PostgreSQL + EF Core/Npgsql
- JWT authentication with customer/admin roles
- Paytm integration boundary with fail-closed payment verification
- SMTP email + configurable WhatsApp HTTP provider notifications
- Vercel-ready frontend and Docker-ready API

## Customer flow

Browse without an account -> product detail/gallery -> add to cart -> login/register -> checkout -> Indian delivery address -> order -> payment -> order history.

The prototype uses a 7–15 day delivery estimate.

## Admin flow

Admin login -> dashboard -> product CRUD -> stock/price/category management -> image URL management -> order listing -> order status updates.

Admin endpoints require the `ADMIN` role; normal customers cannot access them.

## Local development

1. Start PostgreSQL:

```bash
docker compose up -d postgres
```

2. Start the API:

```bash
cd backend
 dotnet run --project src/ECom.Api
```

3. Start the frontend:

```bash
cd frontend
npm install
npm run dev
```

Set `VITE_API_BASE_URL` when the API is not running at `http://localhost:5000`.

## Admin bootstrap

Set `Admin:Email` and `Admin:Password` as environment variables or configuration before the first API startup. The API hashes the password and creates the admin only when that email does not already exist.

## Payment and notifications

Never commit merchant credentials, database passwords, JWT secrets, SMTP passwords or WhatsApp tokens.

Paytm is deliberately fail-closed until the merchant's real gateway/checksum verification is configured. The repository contains the payment service boundary and callback state machine, but a real transaction cannot be enabled without the merchant's Paytm configuration.

Email and WhatsApp notifications are disabled when their provider settings are empty. The WhatsApp integration expects a provider endpoint accepting `{ to, message }` with a Bearer token; adapt the payload in `NotificationService` to the selected provider.

## Deployment

- Frontend: deploy `frontend/` as a Vercel project. `VITE_API_BASE_URL` should point to the deployed API.
- API: deploy `backend/src/ECom.Api` using the included Dockerfile to a container host.
- PostgreSQL: use a managed PostgreSQL provider and set `ConnectionStrings__DefaultConnection`.
- Set `Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience`, `Admin__Email`, `Admin__Password` and integration secrets in the host's secret/environment settings.

## Security notes

- Passwords are PBKDF2 hashed.
- Customer order/cart APIs require authentication.
- Admin APIs require the `ADMIN` role.
- Product prices and stock are re-read server-side during order creation.
- Checkout is transactional.
- Payment callbacks do not mark an order paid unless gateway verification succeeds.
- Secrets are kept outside source control.
