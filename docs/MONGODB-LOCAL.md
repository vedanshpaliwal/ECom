# Local MongoDB setup

The API now uses MongoDB through the official MongoDB Entity Framework Core provider. MongoDB is a document database, so the application stores each aggregate in its own collection and uses explicit ID references between collections.

## Start MongoDB

From the repository root:

```bash
docker compose up -d mongodb
```

MongoDB listens on `mongodb://localhost:27017` and the application database is `ecom`.

## Start the API

```bash
cd backend/src/ECom.Api
dotnet restore
dotnet run
```

On startup the API seeds:

- Woolen Flowers
- Flower Bouquets
- Resin Art
- 6 prototype products
- Product placeholder images
- A development admin account from `Admin:Email` and `Admin:Password`

The seed is idempotent: existing category slugs and product SKUs are not duplicated.

## Development admin

The checked-in development settings use:

```text
Email: admin@ecom.local
Password: ChangeMe123!
```

Change these values before using the application anywhere other than local development.

## Production

Use MongoDB Atlas (or another managed MongoDB deployment) and configure `MongoDB:ConnectionString` and `MongoDB:Database` as environment variables/secrets. Do not commit Atlas credentials.

The application no longer requires PostgreSQL or Npgsql.
