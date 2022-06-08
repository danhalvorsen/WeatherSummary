using System.Diagnostics;
using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL
{
    public class AddWeatherDataForCityCommand : BaseWeatherForecastQuery
    {
        private readonly IFactory factory;

        public AddWeatherDataForCityCommand(IConfiguration config, IFactory factory) : base(config)
        {
            this.factory = factory;
        }


        public async Task GetWeatherDataForCity(CityDto city, IGetWeatherDataStrategy<WeatherForecastDto> strategy) // Adding without date -> backgroundworker uses this
        {
            Debug.Assert(city != null, "city is null");

            try
            {
                var weatherForecastData = await strategy.GetWeatherDataFrom(city, DateTime.Now);
                AddWeatherDataToDatabaseForCityQuery(weatherForecastData, city);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task GetWeatherDataForCity(DateTime dateQuery, CityDto city, IGetWeatherDataStrategy<WeatherForecastDto> strategy) // Adding based on date
        {
            Debug.Assert(city != null, "city is null");

            try
            {
                var weatherForecastData = await strategy.GetWeatherDataFrom(city, dateQuery);
                AddWeatherDataToDatabaseForCityQuery(weatherForecastData, city);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateWeatherDataForCity(DateTime dateQuery, CityDto city, IGetWeatherDataStrategy<WeatherForecastDto> strategy)
        {
            Debug.Assert(city != null, "city is null");
            
            try
            {
                var weatherForecastData = await strategy.GetWeatherDataFrom(city, dateQuery);
                UpdateWeatherDataToDatabaseForCityQuery(weatherForecastData, city, dateQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private WeatherForecastDto AddWeatherDataToDatabaseForCityQuery(WeatherForecastDto addWeatherData, CityDto cityData)
        {
            string queryString = $"DECLARE @city_id INT " +
                                    $"DECLARE @source_id INT " +
                                        $"DECLARE @fk_weatherdata_id INT " +
                                            $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                                $"SELECT @city_id = Id FROM City WHERE City.Name = '{cityData.Name}' " +
                                                    $"SELECT @source_id = id FROM [Source] WHERE [Source].[Name] = '{addWeatherData.Source.DataProvider}' " +
                                                        $"INSERT INTO WeatherData([Date], WeatherType, Temperature, Windspeed, WindDirection, WindspeedGust, Pressure, Humidity, " +
                                                            $"ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, FK_CityId) " +
                                    $"VALUES('{addWeatherData.Date}', '{addWeatherData.WeatherType}', {addWeatherData.Temperature}, {addWeatherData.Windspeed}, " +
                                        $"{addWeatherData.WindDirection}, {addWeatherData.WindspeedGust}, {addWeatherData.Pressure}, {addWeatherData.Humidity}, {addWeatherData.ProbOfRain}, " +
                                            $"{addWeatherData.AmountRain}, {addWeatherData.CloudAreaFraction}, {addWeatherData.FogAreaFraction}, {addWeatherData.ProbOfThunder}, @city_id)" +

                                    $"SELECT @fk_weatherdata_id = Id From WeatherData " +
                                        $"INSERT INTO SourceWeatherData(ConnectionDate, FK_SourceId, FK_WeatherDataId) " +
                                            $"VALUES('{addWeatherData.Date}', @source_id, @fk_weatherdata_id)";

            return InsertIntoDatabase(addWeatherData, queryString);
        }

        private WeatherForecastDto UpdateWeatherDataToDatabaseForCityQuery(WeatherForecastDto addWeatherData, CityDto cityData, DateTime dateToBeUpdated)
        {
            string queryString = $"UPDATE WeatherData " +
                                    $"SET [Date] = '{addWeatherData.Date}', " +
                                    $"WeatherType = '{addWeatherData.WeatherType}', " +
                                    $"Temperature = {addWeatherData.Temperature}, " +
                                    $"Windspeed = {addWeatherData.Windspeed}, " +
                                    $"WindDirection = {addWeatherData.WindDirection}, " +
                                    $"WindspeedGust = {addWeatherData.WindspeedGust}, " +
                                    $"Pressure = {addWeatherData.Pressure}, " +
                                    $"Humidity = {addWeatherData.Humidity}, " +
                                    $"ProbOfRain = {addWeatherData.ProbOfRain}, " +
                                    $"AmountRain = {addWeatherData.AmountRain}, " +
                                    $"CloudAreaFraction = {addWeatherData.CloudAreaFraction}, " +
                                    $"FogAreaFraction = {addWeatherData.FogAreaFraction}, " +
                                    $"ProbOfThunder = {addWeatherData.ProbOfThunder} " +
                                        $"FROM WeatherData " +
                                            $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                $"WHERE CAST([Date] as Date) = '{dateToBeUpdated.Date}' AND City.Name = '{cityData.Name}'";

            return InsertIntoDatabase(addWeatherData, queryString);
        }
    }
}
