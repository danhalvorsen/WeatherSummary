﻿using System.Data.SqlClient;
using WeatherWebAPI.Controllers;

namespace WeatherWebAPI.Factory.Strategy.Database
{
    public class AddCityToDatabaseStrategy : IAddCityToDatabaseStrategy
    {
        private readonly IDatabaseConfig _config;

        public AddCityToDatabaseStrategy(IDatabaseConfig config)
        {
            this._config = config;
        }

        public async Task Add(List<CityDto> city)
        {
            string queryString = $"INSERT INTO City ([Name], Country, Altitude, Longitude, Latitude) " +
                        $"VALUES('{city[0].Name}', '{city[0].Country}', {city[0].Altitude}, {city[0].Longitude}, {city[0].Latitude})";

            using SqlConnection connection = new(_config.ConnectionString);
            SqlCommand command = new(queryString, connection);
            connection.Open();

            await command.ExecuteNonQueryAsync();
        }
    }
}
