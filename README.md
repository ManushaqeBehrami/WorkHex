# HexaFullStack
Full-stack starter (React + Vite + Tailwind + SignalR) + (ASP.NET Core 8, EF Core MySQL, MongoDB) using Hexagonal/Clean architecture.

## Prereqs
- .NET 8 SDK
- Node 18+ & npm
- Docker Desktop (for MySQL & Mongo)

## Quickstart
```bash
# Databases
docker run -d --name mysql -e MYSQL_ROOT_PASSWORD=your_pwd -e MYSQL_DATABASE=hexafullstack -p 3306:3306 mysql:8
docker run -d --name mongo -p 27017:27017 mongo:6

# Backend
dotnet restore HexaFullStack.sln
dotnet ef migrations add Init -p src/HexaFullStack.Infrastructure -s src/HexaFullStack.Api
dotnet ef database update -p src/HexaFullStack.Infrastructure -s src/HexaFullStack.Api
dotnet run --project src/HexaFullStack.Api

# Frontend
cd web/client
npm install
npm run dev
```
API base: `https://localhost:5001` (adjust if needed). SignalR hub: `/hubs/notifications`.
