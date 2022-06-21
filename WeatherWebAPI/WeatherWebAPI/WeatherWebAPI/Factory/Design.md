```mermaid 
classDiagram 
    
    class IGetWeatherDataStrategy {
        <<interface>>

        + GetWeatherDataFrom(CityDto city, DateTime queryDate) List~T~
        + GetDataSource()
    }
    IGetWeatherDataStrategy <|-- IOpenWeatherStrategy
    IGetWeatherDataStrategy <|-- IYrStrategy

        class IGetCityDataStrategy {
        <<interface>>

        + GetCityDataFor(string city) List~T~
    }
    IGetCityDataStrategy <|-- IOpenWeatherStrategy


    class IYrStrategy {

        + List<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
    }
    IYrStrategy <|-- YrStrategy
    YrStrategy -- YrConfig

    class YrStrategy {

        - YrConfig _yrConfig
        + YrStrategy(YrConfig config)

        + List<WeatherForecastDto> GetWeatherDataFrom(Citydto city, DateTime queryDate)
        + string GetDataSource()
    }


    class IOpenWeatherStrategy {

        + List<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        + List<CityDto> GetCityDataFor(string city)
    }
    IOpenWeatherStrategy <|-- OpenWeatherStrategy
    OpenWeatherStrategy -- OpenWeatherConfig

    class OpenWeatherStrategy {

        - OpenWeatherConfig _openWeatherConfig
        + OpenWeatherConfig(OpenWeatherConfig config)

        + List<WeatherForecastDto> GetWeatherDataFrom(CityDto city, DateTime queryDate)
        + List<CityDto> GetCityDataFor(string city)

        + string GetDataSource()
    }


    class YrConfig {

        + string DataSource
        + Uri BaseUrl
        + Uri HomePage

        - MapperConfiguration _mapperConfig
        + MapperConfiguration MapperConfig
        + MapperConfiguration Get(DateTime queryDate)
    }
    YrConfig -- HttpConfig


      class OpenWeatherConfig {

        + string DataSource
        + Uri BaseUrl
        + Uri BaseGeoUrl
        + Uri HomePage
        - MapperConfiguration _mapperConfig
        + MapperConfiguration MapperConfig
        + MapperConfiguration Get(DateTime queryDate)
    }
    OpenWeatherConfig -- HttpConfig


      %%class OpenWeatherCityConfig {
       %%  + HttpConfig httpConfig
       %% - MapperConfiguration mapperConfig
       %% + MapperConfiguration MapperConfig
       %% MapperConfiguration Get(object? queryDate)
    %%}
    %%OpenWeatherCityConfig -- HttpConfig


    class HttpConfig {

        + string DataSource
        + Uri BaseURL
        + Uri HomePage
    }


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