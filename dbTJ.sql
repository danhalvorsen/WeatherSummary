CREATE TABLE City (
    CityID INT,
    City VARCHAR,
    Country VARCHAR,
    Longitude FLOAT,
    Altitude FLOAT,
    Latitude FLOAT,
    PRIMARY KEY (CityID)
);

CREATE TABLE ForecastData (
    ForecastID INT,
    CityID INT not NULL,
    WebsiteID INT not NULL,
    ForecastDate TIMESTAMP,
    Temperature FLOAT,
    Windspeed FLOAT,
    WindDirection INT,
    WindspeedGust FLOAT,
    Pressure FLOAT,
    Humidity FLOAT,
    ProbOfRain FLOAT,
    AmountRain FLOAT,
    CloudAreaFraction FLOAT,
    FogAreaFraction FLOAT,
    ProbOfThunder FLOAT,
    PRIMARY KEY (ForecastID),
    FOREIGN KEY (CityID) REFERENCES City(CityID),
    FOREIGN KEY (WebsiteID) REFERENCES Forecast_data_website(WebsiteID)
);

CREATE TABLE ForecastWebsite (
    WebsiteID INT,
    WebsiteName VARCHAR,
    WebURL VARCHAR,
    Auth VARCHAR,
    PRIMARY KEY (WebsiteID)
);

CREATE TABLE Forecast_data_website (
    Id SERIAL,
    ForecastID INT NOT NULL,
    WebsiteID INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (ForecastID) REFERENCES ForecastData(ForecastID),
    FOREIGN KEY (WebsiteID) REFERENCES ForecastWebsite(WebsiteID)
);

CREATE TABLE Admin (
    AdminID INT,
    FullName VARCHAR,
    Username VARCHAR,
    UserPassword VARCHAR,
    Email VARCHAR,
    PRIMARY KEY (AdminID)
);

CREATE TABLE WeatherType (
    WeatherID INT,
    ForecastID INT NOT NULL,
    Cloudy Boolean,
    Sunny Boolean,
    Rainy Boolean,
    Snowy Boolean,
    Stormy Boolean,
    PRIMARY KEY (WeatherID),
    FOREIGN KEY (ForecastID) REFERENCES ForecastData(ForecastID)
);