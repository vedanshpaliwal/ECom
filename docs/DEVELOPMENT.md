# Local development

1. Start PostgreSQL:
   `docker compose up -d postgres`
2. Start API from `backend`:
   `dotnet restore` then `dotnet run --project src/ECom.Api`
3. Start frontend from `frontend`:
   `npm install` then `npm run dev`

API health endpoint: `/api/health`
Swagger: `/swagger`

## Initial database

The EF Core model currently contains users, categories, products and product images. Orders, carts, payments, addresses, audit logs and notifications are planned in the next foundation iterations.
