using WeatherWebAPI.Controllers;
using WeatherWebAPI.Factory;
using WeatherWebAPI.Factory.Strategy.Database;
using WeatherWebAPI.Query;

namespace WeatherWebAPI.DAL.BackgroundService
{
    public class BackGroundServiceCalculateScore : BaseGetWeatherForecastCommands
    {
        public BackGroundServiceCalculateScore(IConfiguration config, IFactory factory) : base(config, factory)
        {

        }

        public async Task CalculateScore()
        {
            var getCitiesQuery = new GetCitiesQuery(_config);

            try
            {

                _citiesDatabase = await getCitiesQuery.GetAllCities();

                string getActualWeather = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                                $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                            $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                                $"AND CAST(DateForecast as date) = CAST([Date] as date) AND City.Name = '{_citiesDatabase[0].Name}' " +
                                                                    $"AND[Date] BETWEEN DATEADD(day,-7, GETDATE()) AND GETDATE() " +
                                                                        $"ORDER BY[Date], SourceName";

                string getPredictedWeather = $"SELECT WeatherData.Id, [Date], WeatherType, Temperature, Windspeed, WindspeedGust, WindDirection, Pressure, Humidity, ProbOfRain, AmountRain, CloudAreaFraction, FogAreaFraction, ProbOfThunder, DateForecast, " +
                                                $"City.[Name] as CityName, [Source].[Name] as SourceName FROM WeatherData " +
                                                    $"INNER JOIN City ON City.Id = WeatherData.FK_CityId " +
                                                        $"INNER JOIN SourceWeatherData ON SourceWeatherData.FK_WeatherDataId = WeatherData.Id " +
                                                            $"INNER JOIN[Source] ON SourceWeatherData.FK_SourceId = [Source].Id " +
                                                                $"AND CAST(DateForecast as date) != CAST([Date] as date) AND City.Name = '{_citiesDatabase[0].Name}' " +
                                                                    $"AND[Date] BETWEEN DATEADD(day,-7, GETDATE()) AND GETDATE() " +
                                                                        $"ORDER BY[Date], SourceName";

                IGetWeatherDataFromDatabaseStrategy getWeatherDataFromDatabaseStrategy = _factory.Build<IGetWeatherDataFromDatabaseStrategy>();
                var ActualWeather = getWeatherDataFromDatabaseStrategy.Get(getActualWeather);
                var PredictedWeather = getWeatherDataFromDatabaseStrategy.Get(getPredictedWeather);
                
                //var subResult = ActualWeather
                //    .Where(i => PredictedWeather.Any(p => p.DateForecast == i.Date) && i.Source.DataProvider == "Yr")
                //    .ToList();

                //subResult.Sum(i => i.Pressure);

                foreach (var actual in ActualWeather)
                {
                    foreach (var predicted in PredictedWeather)
                    {
                        if(actual.Date == predicted.DateForecast && actual.Source.DataProvider == predicted.Source.DataProvider)
                        {
                            var sumActual = SumWeatherScoreVariables(actual);
                            var sumPredicted = SumWeatherScoreVariables(predicted);
                            var difference = Math.Abs(sumActual - sumPredicted);

                            var score = Math.Round(CalculatePercentage(sumActual, difference), 2);
                            Console.WriteLine("Score: " + score);
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static double CalculatePercentage(double sumActualWeather, double difference)
        {
            return (sumActualWeather - difference) / sumActualWeather * 100;
        }

        private static double SumWeatherScoreVariables(WeatherForecastDto forecast)
        {
            return Math.Abs(forecast.Temperature + forecast.Windspeed + forecast.WindDirection +
                                forecast.Pressure + forecast.Humidity + forecast.ProbOfRain + forecast.AmountRain + 
                                    forecast.CloudAreaFraction);
        }
    }
}
