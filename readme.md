# Library System

## How to start

- You will need Docker Desktop.
- All the configuration details are in configuration files.
- Open the solution, set `docker-compose` as startup project and hit F5.

## Architecture

A multilayer API built using CQRS and Clean Architecture principles:

### Core

- Business logic representing the library domain.

### Application

- The use cases are built in the Application project. This is where the CQRS is implemented.
- Interfaces relevant for executing use cases.

### Common

- Some common concerns like exceptions, paging, base classes etc are in the Common project.
- Repository interfaces are here as well. These could go into Application or Core as well, but I wanted to make it a bit more flexible so any future business core project has access as well.

### Data

- Repository approach is taken. It is built around a generic repository doing basic CRUD operations. Upon this a developer can create additional, entity-specific repository with more involved queries (such as `IUserRepository`).
- Database access is achieved using Entity Framework Core.

### Infrastructure

- Deals with more technical concerns: talking to an external services (MicroBlink Api), validating the incoming data from the MicroBlink Api.
- This is where talking to a message broker, an email service and caching would be implemented.

### Api

- In charge of HTTP communication with the outside world. It talks to the Application layer using Commands, Queries and `MediatR` library.
- Restful, however I also return User Contact along with the User because I feel these User Contacts don't make much sense in being queried by themselves. Manipulating User Contacts is done through a dedicated User Contacts endpoint.
- Has exception handling middleware implemented. Whenever a custom exception is thrown from Core/Application layer, it gets caught and transformed into a proper response: 409 on invalid deletes, 422 on invalid createsor updates. If the exception does not make any business sense, we log and return a 500. All error messages (4xx, 5xx) are returned using the Problem Details standard (https://datatracker.ietf.org/doc/html/rfc7807).
- Api has a health check endpoint.
- Validating inputs is done using `FluentValidation`.

### Talking across layers

- Each layer (Application, Api) has its own set of models/view models. Such an approach requires effort in creating mapping between data structures. In a simple system such as Library System this is an overkill due to extensive overlap between structures and no properties to differentiate between models on each layer. I believe a system of any non-trivial complexity benefits from having separate data containers for moving data from layer to layer, because as time moves on UI requirements will most definitely become more sophisticated and then the isolation of layers will pay off by not having to change core layers as much. Mappings are being done using `AutoMapper`.

### Libraries

- Mapping is done using .
- CQRS is enabled with `MediatR`
- Talking to the database using `Entity Framework Core`
- Validating inputs using `FluentValidation`.

## Database

- Dockerized PostgreSql. Once started, just log into the pgAdmin dashboard and enter `rootpw` as password. Don't forget to install pgAdmin if you don't have one.
- The data is seeded on startup using `AutoFaker`.
- Multicolumn index is created on `RentEvent` to support the first query mentioned in the task.

## Logging

- Logging is implemented using `Serilog`.
- It logs to the console only, but could me made to log into a dedicated log management system easily.
- Log scopes are defined through a `LoggingFilter` which has been added to every controller from `Startup.cs`.

## MicroBlink Api integration

- Done from the `Infrastructure` project. An image is uploaded to `/api/users/identitycard` endpoint and sent to MicroBlink Api. Once the parsed data is returned it's check digits are validated and a `User` is created.
- Even though documentation states 1920x1080 is the stated image size Api accepts, I had to downgrade the quality to make LibrarySystem integrate with MicroBlink Api.
- Api keys are defined in the `appsettings.Development.json`. This is for development purposes only, in real life development such data would be in `.env` file or in user secrets file and definitely not stored in the code repository.

## Postman Collection

- `GET`, `PUT` and `DELETE` requests are chained. In order to use them you first have to execute the `GET Users` request for it to pick up `User.Id` and `UserContact.Id`. If you delete an entity, execute `GET Users` again so it can pick up next set of identifiers.

## What is in here and does not belong to a production system?

- `.Seed()` migrates the database on its own and seeds the data.
- Migrations should be done using a dedicated tool (e.g. `DbUp`).
- Configuration data is in configuration files. Passwords, client secrets etc. should go into `.env` file or in some platform-specific key vault.

## What is missing?

- Authentication and authorization
- Caching
- Message broker
- Proper logging management system, e.g. `Seq`, `Graylog`.
- Unit tests are not implemented. Unit testing should definitely be done on `Core`. Smoke tests would be great as well, just to make sure everything is ok. Those two sets of tests would be an appropriate minimum.
