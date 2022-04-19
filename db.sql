create database weatherForecast;

CREATE TABLE city (
    CityID int,
    Name char not null,
    Country char,
    Longitude int,
    Altitude int,
    Latitude, 
    Primary KEY (CityID)
);

CREATE TABLE forecastWebsite (
    WebsiteID int,
    WebsiteName char,
    Url varchar,
    Authorization varchar,
    Primary KEY (WebsiteID)
);

CREATE TABLE forecastData (
    ForecastID int,
    CityID int,
    WebsiteID int,
    Timestamp datetime,
    Temperature int,
    Windspeed int,
    WindDirection int,
    WindspeedOfGust int,
    Pressure int,
    Humidity int,
    Probability of rain int,
    RainAmount int,
    CloudAreaFraction int,
    FogAreaFraction int,
    ProbabilityOfThunder int,
    Primary KEY (ForecastID),
    FOREIGN KEY (CityID) REFERENCES City(CityID),
    FOREIGN KEY (WebsiteID) REFERENCES forecastWebsite(WebsiteID)
);

create table forecast_data_website (
    ForecastID int,
    WebsiteID int,
    Primary KEY (ForecastID, WebsiteID),
    FOREIGN KEY (ForecastID) REFERENCES forecastData(ForecastID),
    FOREIGN KEY (WebsiteID) REFERENCES forecastWebsite(WebsiteID)
);

create table weatherType (
    WeatherId int,
    ForecastID int,
    Cloudy bool,
    Sunny bool,
    Rainy bool,
    Snowy bool,
    Stormy bool,
    Primary KEY (WeatherId),
    FOREIGN KEY (ForecastID) REFERENCES forecastData(ForecastID)
);

create table admin (
    AdminID int,
    Username char,
    Password char,
    Email char,
    Primary KEY (AdminID)
);
