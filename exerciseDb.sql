CREATE TABLE City (
    CityID INT,
    City VARCHAR,
    Country VARCHAR,
    Longitude FLOAT,
    Altitude FLOAT,
    Latitude FLOAT,
    PRIMARY KEY (CityID)
);



CREATE TABLE ForecastWebsite (
    WebsiteID INT,
    WebsiteName VARCHAR,
    WebURL VARCHAR,
    Auth VARCHAR,
    PRIMARY KEY (WebsiteID)
);

CREATE TABLE ForecastData (
    ForecastID INT,
	PRIMARY KEY (ForecastID),
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
   
	FK_CityID INT,
	FK_WebsiteID INT,
    FOREIGN KEY (FK_CityID) REFERENCES City(CityID),
    --FOREIGN KEY (FK_WebsiteID) REFERENCES Forecast_data_website(Id)
);

CREATE TABLE Forecast_data_website (
    Id INT,
    PRIMARY KEY (Id),
	FK_ForecastID INT,
	FK_WebsiteID INT,
    FOREIGN KEY (FK_ForecastID) REFERENCES ForecastData(ForecastID),
    FOREIGN KEY (FK_WebsiteID) REFERENCES ForecastWebsite(WebsiteID)
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
    Cloudy BIT ,
    Sunny BIT ,
    Rainy BIT ,
    Snowy BIT ,
    Stormy BIT ,
    PRIMARY KEY (WeatherID),
	FK_ForecastID INT,
    FOREIGN KEY (FK_ForecastID) REFERENCES ForecastData(ForecastID)
);


ALTER TABLE ForecastData 
	ADD CONSTRAINT FK_WebsiteID 
	FOREIGN KEY (FK_WebsiteID) REFERENCES Forecast_data_website(Id)
