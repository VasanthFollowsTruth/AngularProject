# Angular + .NET Contact Manager

A full-stack Contact Management application built using Angular 18 and ASP.NET Core 8 Web API with SQL Server.

## Tech Stack

### Frontend
- Angular 18
- TypeScript
- Bootstrap 5
- RxJS

### Backend
- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server
- JWT Authentication
- FluentValidation
- AutoMapper
- Serilog Logging

### Testing
- xUnit
- Moq
- WebApplicationFactory

## Features

- JWT Authentication
- CRUD Operations for Contacts
- Pagination & Sorting
- Responsive UI (Mobile Friendly)
- Add / Edit / Delete via Popup Modals
- Persistent Highlight for Recently Added Record
- Global Exception Handling
- Centralized Error Interceptors
- Backend Logging (Serilog)
- Unit Tests
- Seeded Test Data

##  Project Structure
AngularProject
│
├── backend
│   ├── AngularProject.API
│   └── AngularProject.API.Tests
│
├── frontend
│   └── angular-project-ui
│
└── README.md

## Prerequisites

Make sure the following are installed:

- Node.js (v18+)
- Angular CLI (v18+)
- .NET SDK 8.0+
- SQL Server (LocalDB / Express / Full)
- Git

Check versions in terminal (Run one by one)
node -v
ng version
dotnet --version

## Setup Instructions

### Backend Setup (.NET API)
1. Clone Repository
git clone <github-repo-url>

Open terminal and navigate to- 
cd backend/AngularProject.API

2. Configure Database

Open:
appsettings.json

Update connection string:
"ConnectionStrings": {
  "Default": "Server=.;Database=AngularProjectDb;Trusted_Connection=True;TrustServerCertificate=True"
}

Modify if using SQL authentication.

3. Apply Migrations
dotnet ef database update

This will:

Create database

Create tables

Seed test data

4. Run Backend
dotnet run
API will start at: http://localhost:5008

Swagger: http://localhost:5008/swagger

5. Run the auth login API to get token for authentication
input: (enter this exactly)
{
  "email": "admin@test.com",
  "password": "123456"
}

Copy token from this API output and paste it in the swagger authentication input.
NOTE: THIS IS A MUST TO CHECK THE CONTACTS API

### Frontend Setup (Angular)
1. Open another new terminal and navigate to-
cd frontend/angular-project-ui

2. Install Dependencies
npm install

3. Configure API URL
Open:
src/environments/environment.ts

Ensure if this matches with your localhost api URL:
export const environment = {
        apiUrl: 'http://localhost:5008/api'
    };

4. Run Frontend and backend simultaneously in two different terminals
1st terminal - run backend

navigate to - cd backend/AngularProject.API
then - dotnet run


2nd terminal - run frontend

navigate to - cd frontend/angular-project-ui
then - ng serve
Application will start at: http://localhost:4200

5. Test Credentials

Use below credentials for login:

Email: admin@test.com
Password: 123456

### Running Unit Tests
Navigate to:
cd backend/AngularProject.API.Tests

Run:
dotnet test

### Logging
Serilog logs are stored in: backend/AngularProject.API/Logs/

### Screenshots
1. Default home page (http://localhost:4200/)
![alt text](<UI screenshots/image-2.png>)

2. Login page (http://localhost:4200/login)
![alt text](<UI screenshots/image-3.png>)

3. Contacts page (http://localhost:4200/contacts)
![alt text](<UI screenshots/image-4.png>)

Pagination: 10 entries per page
![alt text](<UI screenshots/image-5.png>)

4. Add/Edit contact modal
![alt text](<UI screenshots/image8.png>)

5. Swagger APIs
![alt text](<UI screenshots/image.png>)

6. Serilog log file will be auto generated when any api has been executed
![alt text](<UI screenshots/image6.png>)

7. 10 unit test cases passed - terminal output
![alt text](<UI screenshots/image7.png>)
