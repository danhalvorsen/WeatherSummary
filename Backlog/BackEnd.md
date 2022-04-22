## Back end
### Configuration of data sources (e.g. yr.no)

- As an admin, I want to set up my own data sources, so that I can configure my system
    - Set up configuration file for data sources (eg www.yr.no, credentials)
    - Create HttpClient for Yr to communicate (by using openAPI contract)
    - Set up authentication for httpClient (please use httpClientFactory)

- As a system, I need a data model for storing data
    - Draw.io to draw database
    - Code first data model (entity framework, EFC) (datamodel context, push changes to database)
    - Create a dataservice that lets us write data to database

### API feature (Get, Post, Put, Patch, Delete)

- Routes: 
    - GET /api/weatherforecast/between?from=01.01.2022&to=01.04.2022
    - GET /api/weatherforecast/week/12
    - GET /api/weatherforecast/Day?date=01.02.2022
    - POST /api/weatherforecast/Create 

- Postman? httpClient on VS code?

- Integration test on API

- Fill up database with fake data

- (React application must create a lot of fake data)


[go back](../README.md/#back-end)