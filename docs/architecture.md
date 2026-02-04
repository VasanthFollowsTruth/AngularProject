# AngularProject Architecture

## Stack
- .NET 8 Web API
- EF Core
- SQL Server (CommonDb)
- Angular 18
- Bootstrap 5
- JWT
- Serilog

## Auth
JWT based authentication.

## Logging
Serilog (Console + File)

## API Draft

POST /api/auth/login  
POST /api/auth/register  
GET  /api/users  

## Frontend

App
├── Auth
├── Dashboard
├── Users
└── Shared
