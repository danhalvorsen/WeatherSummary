# WeatherSummary

Shared team for getting weather data from different data sources and present that data into a React App. The backend will be a .net core WebAPI.

# Potensial dotnet template

<https://fullstackhero.net/>
<https://github.com/fullstackhero/dotnet-webapi-boilerplate>

# Mark down using Mermaid

About: [Mermaid](https://mermaid-js.github.io/mermaid/#/)

Install Visual Studio Code Extension: [Markdown Preview Mermaid Support](https://marketplace.visualstudio.com/items?itemName=bierner.markdown-mermaid)

Used to draw different diagrams and for code snippets in README.md files.

Example Diagram:
```mermaid
sequenceDiagram
    WebApp->>Api: Get weather data

```
Example Code snippet:

```csharp
    Console.WriteLine("Hello World!");
```

# Get the database up and running
#### Download SQL Server Management Studio: [SQL Mng Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
#### Download Docker Desktop: [Docker Desktop](https://www.docker.com/products/docker-desktop/)

Documentation & Linux image list: [Docker SQL Server Documentation](https://hub.docker.com/_/microsoft-mssql-server) 

---
#### **Pull the server docker image from Microsoft**
```
docker pull mcr.microsoft.com/mssql/server

docker pull mcr.microsoft.com/mssql/server:2022-latest

docker pull mcr.microsoft.com/mssql/server:2019-latest

... or whatever image of your choosing.
```

mcr.microsoft.com/mssql/server:2019-latest was the latest version available when this project was started. Therefore this image is used throughout the documentation as shown below:

#### **Run SQL Server container *WITH* volume**
Run this command if you need data to be stored.
```docker
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword" -p 1433:1433 -v Sql-server-storage:/var/opt/mssql -d mcr.microsoft.com/mssql/server:2019-latest
```
#### **Run SQL Server container *WITHOUT* volume**
Run this command if you don't need data to be stored.
```docker
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

#### **Create Docker Network**
For docker compose to work, we need to create a network that both the docker containers share.
```docker
docker network create YourNetworkName
```

# Backend setup
[Visual Studio w/ docker-compose](/WeatherWebAPI/WeatherWebAPI/README_VisualStudioSetup.md)

[SQL Server Management Studio](/WeatherWebAPI/WeatherWebAPI/README_SQLServerManagementStudioSetup.md)

[Make Self Signed HTTPS Certificate](/WeatherWebAPI/WeatherWebAPI/README_SelfSignedHttpsCertificate.md)

# Backlog
### Backend
[Backend specs](/Backlog/BackEnd.md/#back-end)
### Web application
[Web specs](/Backlog/WebApp.md)
### Azure devop
[Azure devops](/Backlog/AzDevOps.md)

# Diagrams
[Entity Relationship Diagram](/EntityRelationshipDiagram.MD)

[Http Design Class Diagram](/WeatherWebAPI/WeatherWebAPI/WeatherWebAPI/Factory/HttpDesign.md)

[Sql Design Class Diagram](/WeatherWebAPI/WeatherWebAPI/WeatherWebAPI/Factory/SqlDesign.md)

# API endpoint(s)

```
GET /api/weatherforecast/predictionByDate?DateQuery.Date={date}&CityQuery.City={cityName}

GET /api/weatherforecast/date?DateQuery.Date={date}&CityQuery.City={cityName}

GET /api/weatherforecast/between?BetweenDateQuery.From={fromDate}&BetweenDateQuery.To={toDate}&CityQuery.City={cityName}

GET /api/weatherforecast/week?week={weekNumber}&City={cityName}

GET api/weatherforecast/getCitiesInDatabase
```
