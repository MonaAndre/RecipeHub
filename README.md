# RecipeHub API

A REST API for managing recipes, built with ASP.NET Core 9, Entity Framework Core and PostgreSQL.

---

## Tech Stack

- ASP.NET Core 9 — Minimal API
- Entity Framework Core — Code First with PostgreSQL
- JWT Bearer — Authentication & Authorization
- BCrypt — Password hashing
- Scalar — OpenAPI documentation UI

---

## Getting Started

### 1. Clone the repository

```bash
git clone <repo-url>
cd RecipeHub
```

### 2. Set up User Secrets

The project uses .NET User Secrets for configuration. Run the following commands inside the `RecipeHub/` project folder:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Database=recipehub;Username=YOUR_USER;Password=YOUR_PASSWORD"
dotnet user-secrets set "Jwt:Key" "your-secret-key-minimum-32-characters-long!"
dotnet user-secrets set "Jwt:Issuer" "RecipeHub"
dotnet user-secrets set "Jwt:Audience" "RecipeHub"
dotnet user-secrets set "Jwt:ExpiresInMinutes" "60"
```

### 3. Apply migrations and seed the database

```bash
dotnet ef database update
```

This will create all tables and insert seed data (users, products, recipes, comments).

### 4. Run the API

```bash
dotnet run
```

The API will be available at `http://localhost:5209`.

---

## API Documentation

Scalar (OpenAPI UI) is available at:

```
http://localhost:5209/scalar/v1
```

---

## Krav för G (Godkänt)
- [x] Minst 2 entiteter med minst 1 relation
- [x] EF Core Code First med minst 1 migration
- [x] Seed-data för att kunna testa API:et
- [x] Minst 6 endpoints som täcker alla CRUD-operationer (Create, Read, Update, Delete)
- [x] Korrekt användning av HTTP-metoder (GET, POST, PUT eller PATCH, DELETE)
- [x] Korrekta statuskoder (minst 200, 201, 204, 400, 404)
- [x] Request- och response-modeller (DTOs) som separerar API-kontrakt från databasmodeller
- [x] Felhantering med try/catch som returnerar lämpliga felmeddelanden och statuskoder
- [x] Swagger/OpenAPI-dokumentation som finns tillgänglig på en endpoint (med en frontend)
- [x] Postman-collection som testar samtliga endpoints (exporterad JSON-fil)
- [x] README med instruktioner för att köra projektet och seeda databasen

## Krav för VG (Väl Godkänt)Krav för VG (Väl Godkänt)
- [ ] Utöver samtliga G-krav ska du uppfylla alla följande krav:
- [x] Minst 3 entiteter med minst 1 relation
- [ ] Global felhantering via middleware (ersätter try/catch i enskilda endpoints) med standardiserad felrespons
- [x] Tydlig lagerarkitektur med API-lagret, service-lagret och databas-lagret
- [ ] Rollbaserad autentisering och auktorisering på minst 1 endpoint
- [ ] Minst 3 enhetstester
- [ ] Minst 1 integrationstest
- [ ] Loggning av fel med ILogger


