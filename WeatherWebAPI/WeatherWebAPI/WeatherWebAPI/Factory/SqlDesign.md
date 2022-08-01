```mermaid
    classDiagram
    
    class IDatabaseConfig {
        + string connectionString
    }
    AddCityToDatabaseStrategy -- IDatabaseConfig
    AddWeatherDataToDatabaseStrategy -- IDatabaseConfig
    GetWeatherDataFromDatabaseStrategy -- IDatabaseConfig
    UpdateWeatherDataToDatabaseStrategy -- IDatabaseConfig


    class IAddCityToDatabaseStrategy {
        <<interface>>

        + Add(List<CityDto> city)
    }
    IAddCityToDatabaseStrategy <|-- AddCityToDatabaseStrategy


    class AddCityToDatabaseStrategy {
        + AddCityToDatabaseStrategy(IDatabaseConfig config)
        + Add(List<CityDto> city)
    }


    class IAddWeatherDataToDatabaseStrategy {
        <<interface>>

        + Add(WeatherForecastDto weatherData, CityDto city)
    }
    IAddWeatherDataToDatabaseStrategy <|-- AddWeatherDataToDatabaseStrategy


    class AddWeatherDataToDatabaseStrategy {
        + AddWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        + Add(WeatherForecastDto weatherData, CityDto city)
    }


    class IGetWeatherDataFromDatabaseStrategy {
        <<interface>>

        + List<WeatherForecastDto> Get(string queryString)
    }
    IGetWeatherDataFromDatabaseStrategy <|-- GetWeatherDataFromDatabaseStrategy


    class GetWeatherDataFromDatabaseStrategy {
        + GetWeatherDataFromDatabaseStrategy(IDatabaseConfig config)
        + List<WeatherForecastDto> Get(string queryString)
    }


    class IUpdateWeatherDataToDatabaseStrategy {
        <<interface>>

        + Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
    }
    IUpdateWeatherDataToDatabaseStrategy <|-- UpdateWeatherDataToDatabaseStrategy


    class UpdateWeatherDataToDatabaseStrategy {
        + UpdateWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        + Update(WeatherForecastDto weatherData, CityDto city, DateTime dateToBeUpdated)
    }
    
 ```

 [Go back](/README.md/#diagram)