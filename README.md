# Application Execution Guide

## 1. Prerequisites
- Visual Studio 2022 installed
- Docker Desktop installed and running
- Git installed (optional, for cloning the repository)

## 2. Start Docker Desktop
1. Open Docker Desktop
2. Wait until the system tray icon shows Docker is running (green icon)
3. Check for any Docker Desktop errors

## 3. Start PostgreSQL Database via Docker
1. Open PowerShell or Command Prompt
2. Navigate to the project folder:
```powershell
cd {repo}\backend
```
3. Execute the command to start only PostgreSQL:
```powershell
docker-compose up -d postgres
```
4. Wait for the confirmation message that the container is running

## 4. Configure and Run the Application in Visual Studio
1. Open Visual Studio 2022
2. Open the project solution:
   - Click on "File" > "Open" > "Project/Solution"
   - Navigate to the project folder
   - Select the `Ambev.DeveloperEvaluation.sln` file

## 5. Run Database Migrations
1. In Visual Studio, open the "Package Manager Console":
   - Click on "Tools" > "NuGet Package Manager" > "Package Manager Console"
2. In the console, execute the following commands:
```powershell
Add-Migration InitialCreate
Update-Database
```

## 6. Run the Application
1. In Visual Studio, ensure the `Ambev.DeveloperEvaluation.Api` project is set as the startup project:
   - Right-click on the project in Solution Explorer
   - Select "Set as Startup Project"
2. Press F5 or click the "Start" button (green play icon) to run the application
3. Wait for the application to start and the browser to open automatically

## 7. Verify Everything is Working
1. The browser should automatically open with the URL: `https://localhost:7001/swagger`
2. In the Swagger interface, you'll see all available endpoints
3. Test some basic endpoints like:
   - GET /api/ProductTypes (to list product types)
   - GET /api/Sales (to list sales)


## 9. To Stop the Application
1. To stop the PostgreSQL container:
```powershell
docker-compose down
```