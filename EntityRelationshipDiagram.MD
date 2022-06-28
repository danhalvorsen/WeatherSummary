````mermaid
erDiagram


City  {
    int Id PK "auto incr."
    string Name
    string Country
    float Longitude
    float Latitude
    float Altitude
}
City ||--|| WeatherData : has
WeatherData {
    int Id PK "auto incr."
    int CityId FK "FK_CityId int not null"
    string Weathertype
    DateTime Date
    float Temperature "celsius"
    float Windspeed "m/s"
    float WindDirection "degrees"
    float WindSpeedGust "m/s"
    float Pressure "hPa"
    float Humidity "%"
    float ProbOfRain "%"
    float AmountRain "mm"
    float CloudAreaFraction "%"
    float FogAreaFraction "%"
    float ProbOfThunder "%"
}
WeatherData }|--|{ SourceWeatherData : "can have many sources"
SourceWeatherData {
    int Id PK "auto incr."
    DateTime ConnectionDate

    int SourceId FK "FK_SourceId int not null"
    int WeatheredataId FK "FK_WeatherDataId int not null"
}
SourceWeatherData }|--|{ Source : "can have many forecasts"
Source {
    int Id PK "auto incr."
    string Name
    string URL
    String Authentication
}

Admin {
    Int Id PK "auto incr."
    string Name
    string Username
    string Password
    string Email
}

```