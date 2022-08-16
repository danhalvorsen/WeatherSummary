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

        + Add(WeatherForecast weatherData, CityDto city)
    }
    IAddWeatherDataToDatabaseStrategy <|-- AddWeatherDataToDatabaseStrategy


    class AddWeatherDataToDatabaseStrategy {
        + AddWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        + Add(WeatherForecast weatherData, CityDto city)
    }


    class IGetWeatherDataFromDatabaseStrategy {
        <<interface>>

        + List<WeatherForecast> Get(string queryString)
    }
    IGetWeatherDataFromDatabaseStrategy <|-- GetWeatherDataFromDatabaseStrategy


    class GetWeatherDataFromDatabaseStrategy {
        + GetWeatherDataFromDatabaseStrategy(IDatabaseConfig config)
        + List<WeatherForecast> Get(string queryString)
    }


    class IUpdateWeatherDataToDatabaseStrategy {
        <<interface>>

        + Update(WeatherForecast weatherData, CityDto city, DateTime dateToBeUpdated)
    }
    IUpdateWeatherDataToDatabaseStrategy <|-- UpdateWeatherDataToDatabaseStrategy


    class UpdateWeatherDataToDatabaseStrategy {
        + UpdateWeatherDataToDatabaseStrategy(IDatabaseConfig config)
        + Update(WeatherForecast weatherData, CityDto city, DateTime dateToBeUpdated)
    }
    
 ```

 [Go back](/README.md/#diagram)