Controller
- Contracts
    - SQL commands
        - Mapping
    - SQL queries
        - GetWeatherForecast -> mapping between WeatherForecast and WeatherForecastDto
        - Get Cities
        - Get AverageScores

Background Services
- CalculateScore
- GetWeatherData from Weather Sources (rename?? because of the other query names?)
---
```mermaid
sequenceDiagram
autonumber
participant Background Services
participant Controller
participant Contracts
participant SQL Commands
participant SQL Queries
participant Mapping

loop Every 24h
    Background Services ->>+ Mapping : Mapping the data fetched from dataproviders
    Mapping ->>+ SQL Commands : Get 7 day weather forecast for each city added in our database, and store all the data.
end

loop Once Every 24h
    Background Services ->>+ SQL Commands : Add score to each prediction available for scoring.
end 

Controller ->> Contracts : Specific contracts for database fetching

Contracts ->>+ SQL Queries : Get weather forecast
SQL Queries ->>+ Mapping : Mapping
alt HttpStatusCode: 200 OK
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Return weather forecast 
else HttpStatusCode: 400 BadRequest
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Bad request response
end

Contracts ->>+ SQL Queries : Get all cities in database
SQL Queries ->>+ Mapping : Mapping
alt HttpStatusCode: 200 OK
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Return all cities 
else HttpStatusCode: 400 BadRequest
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Bad request response
end

Contracts ->>+ SQL Queries : Get average score
SQL Queries ->>+ Mapping : Mapping
alt HttpStatusCode: 200 OK
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Return average score
else HttpStatusCode: 400 BadRequest
    Mapping -->> SQL Queries : 
    SQL Queries -->> Contracts : Bad request response
end

Contracts ->>+ SQL Commands : Add city to database
```