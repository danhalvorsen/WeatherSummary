### **Visual Studio Setup**
---

#### **appsettings.json**
```json
{
  "ConnectionStrings": {
    "WeatherForecastDatabase": "Data Source=SqlServer,1433;Initial Catalog=WeatherForecast_dev;User ID=sa; Password=123456a@;Connect Timeout=120;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```
#### **docker-compose.yml**
```yml
version: '3.4'

services:
  weatherwebapi:
    container_name: WeatherWebAPI_Dev
    image: ${DOCKER_REGISTRY-}weatherwebapi
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_HTTPS_PORT=1337
      - ASPNETCORE_Kestrel__Certificates__Default__Password=asdf
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/asdf.pfx
    ports:
      - "81:80"
      - "1337:443"

    build:
      context: .
      dockerfile: WeatherWebAPI/Dockerfile
    networks:
          - weather

    volumes:
      - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
      - ./docker.cert/https/:/https

networks:
    weather:
      external: true
```
#### **docker-compose.override.yml**
The Kestrel Certificate has to be made and put into the root folder of the project. Follow the instructions on the README.md file. "YourSelfMadeCertificate.pdx" and password is the name and password of your choosing when [creating the certificate](/WeatherWebAPI/WeatherWebAPI/README_SelfSignedHttpsCertificate.md) (Self Signed).

The SA_PASSWORD: "SqlServerAdminPassword" is not really the password. The real password is set in the usersecrets file: C:\Users\UserName\AppData\Roaming\Microsoft\UserSecrets

***Remember*** to set the port for your swagger API in the override file (used by Visual Studio). If you set this **ONLY** in the docker-compose.yml file it will be overriden either way. Port set to 5000 below:
```yml
version: '3.4'

services:
  weatherwebapi:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_HTTPS_PORT=1337
      - ASPNETCORE_Kestrel__Certificates__Default__Password=asdf
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/asdf.pfx
    ports:
      - "81:80"
      - "1337:443"
    volumes:
      - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
      - ./docker.cert/https/:/https


```
---
[Go back](/WeatherWebAPI/WeatherWebAPI/Documentation/README.md)