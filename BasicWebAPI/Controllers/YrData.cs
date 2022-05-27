using System;
using System.Collections.Generic;

namespace BasicWebAPI.YR
{
    public class Geometry
    {
        public string type { get; set; }
        public IList<double> coordinates { get; set; }
    }

    public class Units
    {
        public string air_pressure_at_sea_level { get; set; }
        public string air_temperature { get; set; }
        public string air_temperature_max { get; set; }
        public string air_temperature_min { get; set; }
        public string air_temperature_percentile_10 { get; set; }
        public string air_temperature_percentile_90 { get; set; }
        public string cloud_area_fraction { get; set; }
        public string cloud_area_fraction_high { get; set; }
        public string cloud_area_fraction_low { get; set; }
        public string cloud_area_fraction_medium { get; set; }
        public string dew_point_temperature { get; set; }
        public string fog_area_fraction { get; set; }
        public string precipitation_amount { get; set; }
        public string precipitation_amount_max { get; set; }
        public string precipitation_amount_min { get; set; }
        public string probability_of_precipitation { get; set; }
        public string probability_of_thunder { get; set; }
        public string relative_humidity { get; set; }
        public string ultraviolet_index_clear_sky { get; set; }
        public string wind_from_direction { get; set; }
        public string wind_speed { get; set; }
        public string wind_speed_of_gust { get; set; }
        public string wind_speed_percentile_10 { get; set; }
        public string wind_speed_percentile_90 { get; set; }

    }
    public class Meta
    {
        public DateTime updated_at { get; set; }
        public Units units { get; set; }

    }
    public class Details
    {
        public double air_pressure_at_sea_level { get; set; }
        public double air_temperature { get; set; }
        public double air_temperature_percentile_10 { get; set; }
        public double air_temperature_percentile_90 { get; set; }
        public double cloud_area_fraction { get; set; }
        public double cloud_area_fraction_high { get; set; }
        public double cloud_area_fraction_low { get; set; }
        public double cloud_area_fraction_medium { get; set; }
        public double dew_point_temperature { get; set; }
        public double fog_area_fraction { get; set; }
        public double relative_humidity { get; set; }
        public double ultraviolet_index_clear_sky { get; set; }
        public double wind_from_direction { get; set; }
        public double wind_speed { get; set; }
        public double wind_speed_of_gust { get; set; }
        public double wind_speed_percentile_10 { get; set; }
        public double wind_speed_percentile_90 { get; set; }

    }
    public class Instant
    {
        public Details details { get; set; }

    }
    public class SummaryNext12h
    {
        public string symbol_code { get; set; }
        public string symbol_confidence { get; set; }

    }
    public class DetailsNext12h
    {
        public double probability_of_precipitation { get; set; }

    }
    public class Next__hours12
    {
        public SummaryNext12h summary { get; set; }
        public DetailsNext12h details { get; set; }

    }
    public class SummaryNext1h
    {
        public string symbol_code { get; set; }

    }
    public class DetailsNext1h
    {
        public double precipitation_amount { get; set; }
        public double precipitation_amount_max { get; set; }
        public double precipitation_amount_min { get; set; }
        public double probability_of_precipitation { get; set; }
        public double probability_of_thunder { get; set; }

    }
    public class Next__hours1
    {
        public SummaryNext1h summary { get; set; }
        public DetailsNext1h details { get; set; }

    }
    public class SummaryNext6h
    {
        public string symbol_code { get; set; }

    }
    public class DetailsNext6h
    {
        public double air_temperature_max { get; set; }
        public double air_temperature_min { get; set; }
        public double precipitation_amount { get; set; }
        public double precipitation_amount_max { get; set; }
        public double precipitation_amount_min { get; set; }
        public double probability_of_precipitation { get; set; }

    }
    public class Next__hours6
    {
        public SummaryNext6h summary { get; set; }
        public DetailsNext6h details { get; set; }

    }
    public class Data
    {
        public Instant instant { get; set; }
        public Next__hours12 next_12_hours { get; set; }
        public Next__hours1 next_1_hours { get; set; }
        public Next__hours6 next_6_hours { get; set; }

    }
    public class Timeseries
    {
        public DateTime time { get; set; }
        public Data data { get; set; }

    }
    public class Properties
    {
        public Meta meta { get; set; }
        public IList<Timeseries> timeseries { get; set; }

    }
    public class ApplicationYr
    {
        public string type { get; set; }
        public Geometry geometry { get; set; }
        public Properties properties { get; set; }

    }
}