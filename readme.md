# Library System

## How to start

- You will need Docker Desktop.
- All the configuration details are in configuration files.
- Open the solution, set `docker-compose` as startup project and hit F5.

## Architecture

### Database

- Dockerized PostgreSql. Once started, just log into the pgAdmin dashboard and enter `rootpw` as password. Don't forget to install pgAdmin if you don't have one.

### Loggin

- Serilog. Logs straight to the console.

## What is in here and does not belong to a production system?

- `.Seed()` migrates the database on its own and seeds the data.
- Migrations should be done using a dedicated tool (e.g. `DbUp`).
- Configuration data is in configuration files. Passwords, client secrets etc. should go into `.env` file or in some platform-specific key vault.

## What is missing?

- Authentication and authorization
- Caching
- Message broker
- Proper logging management system, e.g. `Seq`, `Graylog`.
