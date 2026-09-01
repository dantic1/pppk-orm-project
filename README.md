# Medical Information System (PPPK ORM Project)
 
A medical information system built with C# .NET 9 and Entity Framework Core 9 for managing patients, doctors, diseases, medications, and examinations.
 
## Prerequisites
 
- .NET 9 SDK
- Docker and Docker Compose
- Rider IDE (or VS Code with C# extensions)
## Getting Started
 
### 1. Start PostgreSQL Database
 
Run the database container:
 
```bash
docker compose up -d
```
 
This starts PostgreSQL 16 on `localhost:5432`.
 
### 2. Apply Migrations
 
From the project root directory:
 
```bash
dotnet ef database update --project PppkOrmProject.Data --startup-project PppkOrmProject.Console
```
 
### 3. Run the Application
 
```bash
cd PppkOrmProject.Console
dotnet run
```
 
The console application will start with a main menu showing options to manage patients, diseases, medications, medical history, prescriptions, and examinations.
 
## Project Structure
 
- **PppkOrmProject.Data** — Data layer with Entity Framework models and migrations
- **PppkOrmProject.Console** — Console application entry point with menu system
## Database Credentials
 
- Username: `doctor`
- Password: `password`
- Database: `hospital`
## Features
 
- CRUD operations on patients, diseases, medications, prescriptions, and examinations
- Doctors initialized at startup (cannot create new doctors after first run)
- Medical history tracking
- Automatic database migrations on startup
- Loading demos (eager, lazy, explicit) in patients menu
## Stopping the Database
 
```bash
docker compose down
```
 
To completely remove the database and start fresh:
 
```bash
docker compose down -v
```

