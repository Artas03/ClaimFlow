# ClaimFlow

ClaimFlow is a training scaffold for an insurance policy and claims management platform, built with .NET 8, ASP.NET Core controllers, EF Core 8, PostgreSQL, and xUnit. This repository contains the solution structure and startup wiring only; domain features are intentionally not implemented.

## Prerequisites

- .NET 8 SDK
- Docker Desktop with Docker Compose

## Local setup

From the repository root:

1. Copy `.env.example` to `.env`, then edit the placeholder password to a local-only value:

   ```sh
   cp .env.example .env
   ```
2. Start PostgreSQL:

   ```sh
   docker compose up -d postgres
   ```

3. Set the API connection string in the current shell using the same values as `.env`:

   ```sh
   export ConnectionStrings__ClaimFlow="Host=localhost;Port=5432;Database=claimflow;Username=claimflow;Password=your-local-password"
   ```

   If you changed the database name, user, or port in `.env`, use those values here. The API requires this setting at startup; it does not connect to the database for the status endpoint.

4. Restore, build, test, and run:

   ```sh
   dotnet restore ClaimFlow.sln
   dotnet build ClaimFlow.sln
   dotnet test ClaimFlow.sln
   dotnet run --project src/ClaimFlow.Api --launch-profile http
   ```

   The status endpoint is `GET http://localhost:5107/api/status`. The `http` launch profile also opens Swagger UI, which is enabled only in the Development environment.

Stop the local database with `docker compose down`. To also remove its local data volume, use `docker compose down -v`.

## Solution layout and dependencies

- `src/ClaimFlow.Domain`: domain layer; no ClaimFlow project references.
- `src/ClaimFlow.Application`: application layer; references Domain.
- `src/ClaimFlow.Infrastructure`: infrastructure adapters and empty `ClaimFlowDbContext`; references Application and Domain; uses EF Core 8 and Npgsql.
- `src/ClaimFlow.Api`: ASP.NET Core controller host; references Application and Infrastructure.
- `tests/ClaimFlow.UnitTests`: xUnit unit-test project; references Domain and Application.
- `tests/ClaimFlow.IntegrationTests`: xUnit integration-test project; references Api and Infrastructure.
- `docs/architecture`: architecture notes.
- `docs/adr`: architecture decision records.

The architecture starts as a modular monolith. See [ADR 0001](docs/adr/0001-modular-monolith.md). GitHub Actions restores, builds, and tests the solution with .NET 8.

## Configuration and secrets

`.env.example` contains placeholders for Compose. Keep the actual `.env` file and connection strings out of version control; `.gitignore` excludes local environment files. No real credentials are provided or committed.

## Scaffold boundaries

No policy, claim, payment, or notification feature; CRUD endpoint; repository; authentication; database model or migration; or production deployment configuration is implemented. These are left for future development tickets.
