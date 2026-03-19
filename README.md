## Krav för G (Godkänt)
[x] Minst 2 entiteter med minst 1 relation
[x] EF Core Code First med minst 1 migration
[x] Seed-data för att kunna testa API:et
[x] Minst 6 endpoints som täcker alla CRUD-operationer (Create, Read, Update, Delete)
[x] Korrekt användning av HTTP-metoder (GET, POST, PUT eller PATCH, DELETE)
[x] Korrekta statuskoder (minst 200, 201, 204, 400, 404)
[x] Request- och response-modeller (DTOs) som separerar API-kontrakt från databasmodeller
[x] Felhantering med try/catch som returnerar lämpliga felmeddelanden och statuskoder
[x] Swagger/OpenAPI-dokumentation som finns tillgänglig på en endpoint (med en frontend)
[x] Postman-collection som testar samtliga endpoints (exporterad JSON-fil)
[] README med instruktioner för att köra projektet och seeda databasen
## Krav för VG (Väl Godkänt)Krav för VG (Väl Godkänt)
[] Utöver samtliga G-krav ska du uppfylla alla följande krav:
[] Minst 3 entiteter med minst 1 relation
[] Global felhantering via middleware (ersätter try/catch i enskilda endpoints) med standardiserad felrespons
[] Tydlig lagerarkitektur med API-lagret, service-lagret och databas-lagret
[] Rollbaserad autentisering och auktorisering på minst 1 endpoint
[] Minst 3 enhetstester
[] Minst 1 integrationstest
[] Loggning av fel med ILogger
