### **Visual Studio Setup**
---

#### **appsettings.json**
```json
{
  "ConnectionStrings": {
    "WeatherForecastDatabase": "Data Source=SqlServer,1433;Initial Catalog=WeatherForecast;User ID=sa; Password=123456a@;Connect Timeout=20;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
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
    container_name: WeatherWebAPI
    image: ${DOCKER_REGISTRY-}weatherwebapi
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_HTTPS_PORT=5000
      - ASPNETCORE_Kestrel__Certificates__Default__Password=YourSelfMadeCertificatePassword
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/YourSelfMadeCertificate.pfx
    ports:
      - "80:80"
      - "5000:443"

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

volumes:
  Sql-server-storage:
    external: true
    
```
#### **docker-compose.override.yml**
The Kestrel Certificate has to be made and put into the root folder of the project. Follow the instructions on the README.md file. "YourSelfMadeCertificate.pfx" and password is the name and password of your choosing when [creating the certificate](/WeatherWebAPI/WeatherWebAPI/README_SelfSignedHttpsCertificate.md) (Self Signed).

To hide passwords to the public, you could add it to your UserSecrets file; ie: 
```csharp
"SQLServerPassword": "123456a@",
"Password": "superpassword123",
```

***Remember*** to set the port for your swagger API in the override file (used by Visual Studio). If you set this **ONLY** in the docker-compose.yml file it will be overriden either way. Port set to 5000 below:
```yml
version: '3.4'

services:
  weatherwebapi:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ASPNETCORE_HTTPS_PORT=5000
      - ASPNETCORE_Kestrel__Certificates__Default__Password=YourSelfMadeCertificatePassword
      - ASPNETCORE_Kestrel__Certificates__Default__Path=/https/YourSelfMadeCertificate.pfx
    ports:
      - "80:80"
      - "5000:443"
    volumes:
      - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
      - ./docker.cert/https/:/https


```
---
[Go back](/WeatherWebAPI/WeatherWebAPI/Documentation/README.md)