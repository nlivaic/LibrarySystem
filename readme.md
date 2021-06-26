# Library System

## How to start

- You will need Docker Desktop.
- All the configuration details are in configuration files.
- Open the solution, set `docker-compose` as startup project and hit F5.

## Architecture

## Architecture

- Log into the pgAdmin dashboard and enter `rootpw` as password.

## What is in here and does not belong to a production system?

- `.Seed()` migrates the database on its own and seeds the data.
- Migrations should be done using a dedicated tool (e.g. `DbUp`).
- Configuration data is in configuration files. Passwords, client secrets etc. should go into `.env` file.
