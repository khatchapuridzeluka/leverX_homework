# FIDE Rating System API

A RESTful ASP.NET Core Web API for managing a FIDE-style chess rating system. This application provides comprehensive CRUD operations for managing players, tournaments, games, and chess openings with JWT-based authentication and role-based access control.

---

##  Tech Stack

- ASP.NET Core Web API
- Dapper -  Lightweight ORM for database operations
- Microsoft SQL Server
- AutoMapper
- Serilog
- Swagger (Swashbuckle)
- JWT Authentication

---

##Features

- CRUD for:
  - Players - Create, read, update, and delete chess players
  - Tournaments - Organize and manage chess tournaments
  - Games - Record and track individual chess games
  - Openings - Manage openings
  - TournamentPlayers - Handle players for tournaments
  - Users - User/Admin
- JWT-based login & role-based access
- Global exception handling & logging via Serilog
- Auto-generated API docs with Swagger

---

##  Getting Started

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Visual Studio 2022
- (Optional) Postman or curl for API testing

### Clone the repository

```bash
git clone https://github.com/khatchapuridzeluka/leverX_homework
cd fide-rating-system
Update the connection string in appsettings.json
Configure JWT Settings
```
### Run the Application
dotnet restore
dotnet run
